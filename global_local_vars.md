# Как избавиться от глобальных переменных в Forth-библиотеке

#### Для использования кода как библиотеки действительно лучше избегать глобальных переменных.

## Вот несколько способов переписать ваш код:

### 1. Используя локальные переменные (современный подход)
``` 
\ Библиотека для работы с файлами без глобальных переменных
\ Использует локальные переменные (доступны в современных Forth системах)

: get-file-size ( c-addr u -- n )
  r/o open-file IF
    ." Не удалось открыть файл" cr
    drop 0 exit
  THEN
  
  { fileid }  \ Локальная переменная
  
  fileid file-size throw d>s
  
  \ Запоминаем размер для возврата
  dup
  
  \ Закрываем файл
  fileid close-file throw
;

: read-file-content ( c-addr u -- )
  r/o open-file IF
    ." Не удалось открыть файл" cr
    drop exit
  THEN
  
  { fileid }  \ Локальная переменная для дескриптора
  
  \ Получаем размер файла
  fileid file-size throw d>s
  
  \ Размер и буфер
  dup { len }
  allocate throw { buf }
  
  \ Читаем файл в буфер
  buf len fileid read-file throw drop
  
  \ Закрываем файл
  fileid close-file throw
  
  \ Выводим содержимое
  buf len type
  
  \ Освобождаем память
  buf free throw
;

: cat ( c-addr u -- )
  2dup r/o open-file IF
    drop
    ." Файл '" type ." ' не найден!" cr
  ELSE
    { fileid }
    fileid close-file throw
    read-file-content
  THEN
;
\ read_file_lib.fs
```

### 2. Используя только стек (традиционный подход)
```
\ Библиотека для работы с файлами без переменных
\ Использует только стек для передачи данных

: get-file-size ( c-addr u -- n )
  r/o open-file IF
    ." Не удалось открыть файл" cr
    drop 0 exit
  THEN
  
  \ fileid на стеке
  dup >r  \ Сохраняем копию fileid для закрытия
  
  file-size throw d>s
  
  \ Закрываем файл
  r> close-file throw
;

: read-and-show-file ( fileid -- )
  \ Сохраняем fileid
  dup >r
  
  \ Получаем размер файла
  file-size throw d>s
  
  \ Выделяем память
  dup >r  \ Сохраняем размер
  allocate throw
  
  \ Буфер на стеке, читаем файл
  dup r@ r@ rot read-file throw drop
  
  \ Выводим содержимое
  dup r@ type
  
  \ Освобождаем память
  dup free throw drop
  
  \ Закрываем файл и очищаем стек
  r> drop r> close-file throw
;

: read-file-content ( c-addr u -- )
  r/o open-file IF
    ." Не удалось открыть файл" cr
    drop exit
  THEN
  
  read-and-show-file
;

: cat ( c-addr u -- )
  2dup r/o open-file IF
    drop
    ." Файл '" type ." ' не найден!" cr
  ELSE
    \ fileid на стеке
    close-file throw
    read-file-content
  THEN
;
\ read_file_stack.fs
```

### 3. Используя отдельный словарь (модульный подход)
```
\ Модульная библиотека для работы с файлами
\ Изолирует свои переменные в отдельном словаре

VOCABULARY FILE-LIB
FILE-LIB DEFINITIONS

\ Эти переменные видны только внутри FILE-LIB
VARIABLE file-handle
VARIABLE file-len
VARIABLE buf-addr

: get-file-size ( c-addr u -- n )
  \ Как в вашем оригинальном коде
  r/o open-file IF
    ." Не удалось открыть файл" cr
    drop 0 exit
  THEN
  file-handle !
  file-handle @ file-size throw d>s
  dup
  file-handle @ close-file throw
;

: read-file-content ( c-addr u -- )
  \ Как в вашем оригинальном коде
  r/o open-file IF
    ." Не удалось открыть файл" cr
    drop exit
  THEN
  file-handle !
  file-handle @ file-size throw d>s
  dup file-len !
  allocate throw buf-addr !
  buf-addr @ file-len @ file-handle @ read-file throw drop
  file-handle @ close-file throw
  buf-addr @ file-len @ type
  buf-addr @ free throw
;

: cat ( c-addr u -- )
  2dup r/o open-file IF
    drop
    ." Файл '" type ." ' не найден!" cr
  ELSE
    close-file throw
    read-file-content
  THEN
;

: cat-info ( c-addr u -- )
  cat
  ." Размер файла: " file-len @ . ." байт" cr
;

FORTH DEFINITIONS

\ Экспортируем только нужные слова в основной словарь
: file-cat ( c-addr u -- ) FILE-LIB::cat ;
: file-info ( c-addr u -- ) FILE-LIB::cat-info ;
: file-size ( c-addr u -- n ) FILE-LIB::get-file-size ;

\ file_lib.fs
```

### Пример использования
```
include file_lib.fs

\ Используем экспортированные функции
s" memory.md" file-cat
s" memory.md" file-info
s" memory.md" file-size . cr
```

### Рекомендации:
* **Вариант с локальными переменными - самый читаемый и поддерживаемый, но требует поддержки локальных переменных Forth-системой**
* **Вариант только со стеком - работает всегда, но сложнее для чтения и отладки**
* **Вариант с отдельным словарем - хороший компромисс, изолирует переменные, но позволяет работать традиционным стилем Forth**
* **Для современного Forth-кода в библиотеках лучше всего использовать комбинацию подходов 1 и 3 - локальные переменные в отдельном словаре.**