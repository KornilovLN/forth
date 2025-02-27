\ --- Оформителькие элементы ---------------

: DEFIS               \ выводит дефис
  45 EMIT
;

: DEFIS_LINE ( n -- ) \ Строка дефисов
  0 DO DEFIS LOOP
;

: SCOBA_LINE ( n -- ) \ рамочное подчерк.
  ." <<< "
  DEFIS_LINE
  ."  >>>"
;

: TITLE_LINE ( addr u n -- ) \ <<< --- Заголовок --- >>>
    ." <<< "
    ROT ROT            \ Перемещаем строку в верх стека
    TYPE               \ Выводим строку
    SPACE              \ Пробел
    DEFIS_LINE         \ Выводим дефисы
    SPACE              \ Пробел
    ." >>>"
;

: TITLE_LINE2 ( addr u n -- ) \ <<< --- Заголовок --- >>>
    ." <<< "
    >R                 \ Сохраняем n в return stack
    2DUP               \ Дублируем addr u 
    R@ 2/ DEFIS_LINE   \ Первая половина дефисов (получаем n с return stack)
    SPACE              \ Пробел
    TYPE               \ Выводим строку
    SPACE              \ Пробел
    R> 2/ DEFIS_LINE   \ Вторая половина дефисов (берем n с return stack)
    SPACE              \ Пробел
    ." >>>"
;

: AUTHOR
35 SCOBA_LINE cr
S" Author: LN Kornilov" 15 TITLE_LINE cr
S" Github: github.com/KornilovLN" 5 TITLE_LINE cr
S" email:  ln.KornilovStar@gmail.com" 1 TITLE_LINE cr
35 SCOBA_LINE cr
;

: PROJECT
35 SCOBA_LINE cr
S" Project: Testing library:" 9 TITLE_LINE cr
21 TITLE_LINE cr
S" data:   26-02-2025   18:40:00" 5 TITLE_LINE cr
35 SCOBA_LINE cr
;