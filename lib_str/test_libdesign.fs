include lib_design.fs

\ --- Тестирование библиотеки отрисовки -------------
\ --- рамочных подчеркиваний и заголовков -----------
\ --- Подключаем библиотеку lib_вуышпт.fs -----------

\ --- Variables -------------------------------------
VARIABLE LEN1
VARIABLE LEN2
VARIABLE LEN3

\ --- Initialization --------------------------------
35 LEN1 !
29 LEN2 !
28 LEN3 !

\ --- Test ------------------------------------------
CR ." ============= AUTHOR, PROJECT =============" CR
AUTHOR CR
S" lib_design.fs" PROJECT

CR ." ============= SCOBA_LINE ==================" CR
LEN1 @ SCOBA_LINE
CR

CR ." ============= TITLE_LINE ==================" CR
S" Title" LEN2 @ TITLE_LINE
CR

CR ." ============= TITLE_LINE2 =================" CR
S" Title" LEN3 @ TITLE_LINE2
CR

bye