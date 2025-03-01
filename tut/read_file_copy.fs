\ Пример чтения файла целиком в память
\ Путь к файлу передается в качестве аргумента
\ Выводится содержимое файла

\ Простое и надежное чтение файла в память и вывод его содержимого
\ Путь к файлу передается в качестве аргумента

VARIABLE file-handle
VARIABLE file-len
VARIABLE buf-addr

: get-file-size ( c-addr u -- )
  \ Открываем файл
  r/o open-file
  IF
    ." Не удалось открыть файл" cr
    drop exit
  THEN
  
  \ Сохраняем дескриптор файла
  file-handle !
  
  \ Получаем размер файла
  file-handle @ file-size throw d>s
  
  \ Запоминаем размер для возврата
  dup
  
  \ Закрываем файл
  file-handle @ close-file throw
;



: read-file-content ( c-addr u -- )
  \ Открываем файл
  r/o open-file 
  IF
    ." Не удалось открыть файл" cr
    drop exit
  THEN
  
  \ Сохраняем дескриптор файла
  file-handle !
  
  \ Получаем размер файла
  file-handle @ file-size throw d>s
  
  \ Сохраняем размер и выделяем память
  dup file-len !
  allocate throw buf-addr !
  
  \ Читаем файл в буфер
  buf-addr @ file-len @ file-handle @ read-file throw drop
  
  \ Закрываем файл
  file-handle @ close-file throw
  
  \ Выводим содержимое
  buf-addr @ file-len @ type
  
  \ Освобождаем память
  buf-addr @ free throw
;


\ Проверяем существует ли файл перед чтением
: cat ( c-addr u -- )
  2dup r/o open-file
  IF
    drop
    ." Файл '" type ." ' не найден в текущей директории!" cr
  ELSE
    close-file throw
    read-file-content
  THEN
;

: cat-info ( c-addr u -- )
  cat
  ." Размер файла: " file-len @ . ." байт" cr
;

: more ( c-addr u -- )
  \ Аналогично cat, но с паузой после каждой страницы
  \ ...
;



\ Пробуем прочитать файл безопасным способом (как в Linux)
s" memory.md" cat
CR

\ И с выводом размера файла
s" memory.md" cat-info

\ DВывод размера файла без чтения файла в память:
s" memory.md" get-file-size . cr

bye