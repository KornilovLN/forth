\ --- Тестирование библиотеки вывода ----------------
\ --- констант и переменных из памяти ---------------

40 CONSTANT LEN1
29 CONSTANT LEN2
28 CONSTANT LEN3

CR ." ============= Вывод констант ==============" CR
LEN1 LEN2 LEN3 . . . CR



\ --- Variables -------------------------------------
VARIABLE VLEN1
VARIABLE VLEN2
VARIABLE VLEN3

\ --- Initialization --------------------------------
40 VLEN1 !
29 VLEN2 !
28 VLEN3 !

CR ." ============= Вывод переменных=============" CR
VLEN1 @ VLEN2 @ VLEN3 @ . . . CR

bye