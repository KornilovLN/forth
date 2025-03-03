# Основные слова Forth 

## Interpreter
```
WORD      HASH    BASE    LOAD      NUMBER
FIND      OCTAL   EXIT    INTERPRET ’
DECIMA    EXECUTE (       ABORT     FORGET
HEX
```
## Terminal
```
.         DUMP    SPACE   SPACES    EMIT
TYPE      EXPECT    DIGIT   CR      KEY           
```
## Data stack
```
DUP       DROP    SWAP    OVER      ABS           
+         -       0<      @         !
*         /       0=      =         +!
*/        */MOD
MIN       MAX     MOD     C@        C!
AND       XOR     OR      NOT       NEGATE     
ATAN      LOG     EXP     SQRT      SIN.COS
```    
## Return stack
```
:         ;       PUSH    POP       I         
```
## Disk
```
BLOCK     BUFFER  UPDATE  PREV      FLUSH
OLDEST
```
## Compiler
```
CREATE    VARIABLE  [     BEGIN     DO
ALLOT     CONSTANT  ]     UNTIL     LOOP
,         SMUDGE  LITERAL AGAIN     +LOOP
."        WHILE   IF      COMPILE   REPEAT
ELSE      THEN                      UNTIL
```
```
\ ------------------------------------------
\ 1+ 1- 2+ 2- 2* 2/       - выполн. быстрее
\ ABORT DECLARE LIST EDIT 
\ LOAD READ GET SEND QUERY ENTER CODE
\ BEGIN END AGAIN EXIT CONTINUE
\ FORGET DIGIT BASE .S
\ Как отличается порядок слов Algol и Forth:
\ Algol — IF expression THEN true ELSE false
\ Forth — stack IF true ELSE false THEN
\===========================================

variable v1      (-- addr)\ let v1: u64;
5 v1 !           (5 --   )\ v1 = 5;
v1 @ .           (  -- 5 )\ out (5)
v1 @ dup + .S    (  -- A )\ temp = v1 + v1
v1 @ 2* .S       (  -- A )\ temp = v1 * 2

create v2 20 cells allot  \ let v2: [u64,20]
v2 20 cells dump          \ dump array v2
3 v2 5 cells + !  ok      \ v2[5] = 3
4 v2 2 cells + !  ok      \ v2[2] = 4  
v2 20 cells dump          \ dump array v2 

create v3                 \ create array
  5 , 4 , 3 , 2 , 1 , ok  \ v3=[5,4,3,2,1];  
v3 @ . 5  ok              \ out (v3[0])
v3 cell+ @ . 4  ok        \ out (v3[1])
v3 2 cells + @ . 3  ok    \ out (v3[2])
v3 3 cells + @ . 2  ok    \ out (v3[3])
v3 4 cells + @ . 1  ok    \ out (v3[4])
v3 5 cells dump           \ dump array v3
 
\ v = v3[0]+v3[1]+v3[2]+v3[3]+v3[4]
\ v = 5+4+3+2+1
variable v redefined v   ok
0 v !  ok
v @ v3 @ + v !   ok
v @ v3 1 cells + @ + v !   ok     
v @ v3 2 cells + @ + v !   ok
v @ v3 3 cells + @ + v !   ok
v @ v3 4 cells + @ + v !   ok
v @ . F  ok  

\ Выделить память на 10 ячеек здесь.
here 10 cells allot
here .          (  -- addr)
\ запомнить начало here в ячейке Mem
variable Mem
here Mem !  ok
\ занести 7 в начало here
7 here ! ok
\ прочитать и вывести 7 можно 3-мя способами
here @ .  7 ok
Mem @ @ .   7 ok
\ занести 6 в след ячейку here
6 here cell+ ! ok
\ занести 9 во 2-ю ячейку here
6 here 2 cells + ! ok
\ вывести dump 2-мя способами 
here 10 cells dump
Mem @ 10 cells dump 
```


