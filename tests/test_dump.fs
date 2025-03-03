include ../lib_str/lib_common.fs
include ../lib_str/lib_design.fs



\ === AUTHOR & PROJECT =========================
CR AUTHOR
CR S" lib_commom.fs" PROJECT
CR FRM-DATETIME CR CR
\ ==============================================

26 CONSTANT SIZE

\ --- Выделяем память под массив из 26 элементов
create v SIZE cells allot  \ let v2: [u64,20]

\ --- Выводим содержимое массива
." --- Выводим содержимое массива v после создания" CR
v SIZE cells dump          \ dump array v2
CR 

\ --- Заполняем массив символом 33 (!) в цикле
." --- Запуск заполнения символом (!) массива с пом. array-fill" CR
33 v SIZE array-fill
CR

\ --- Выводим содержимое массива
." --- Выводим содержимое массива v после заполнения символом (!)" CR
v SIZE cells dump          \ dump array v2
CR

\ --- Заполняем массив значениями от 65 до 90 частично
." --- Заполняем массив v частично буквами" CR
65 v 0 cells + !         \ v2[0] = 65
66 v 1 cells + !         \ v2[1] = 66               
67 v 2 cells + !         \ v2[2] = 67 
68 v 3 cells + !         \ v2[3] = 68
87 v 22 cells + !        \ v2[22] = 87
88 v 23 cells + !        \ v2[23] = 88  
89 v 24 cells + !        \ v2[24] = 89
90 v 25 cells + !        \ v2[25] = 90  
CR

\ --- Выводим содержимое массива
." --- Выводим содержимое массива v после частичного заполнения буквами" CR
v SIZE cells dump          \ dump array v2
CR

\ --- Очищаем массив в цикле
." --- Запуск очистки массива словом array-clear" CR
v SIZE array-clear
CR

\ --- Выводим содержимое массива
." --- Выводим содержимое массива v после очистки" CR
v SIZE cells dump
CR

\ === END TEST =================================

bye