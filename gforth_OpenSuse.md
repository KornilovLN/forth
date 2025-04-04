# Установка gforth на OpenSuse

## Вариант 2: Если нужна последняя версия из исходников

1) Сначала установите необходимые зависимости:
```
sudo zypper install gcc make libtool automake autoconf libffi-devel libtool
```

2) Загрузите и распакуйте исходники:
```
wget https://www.complang.tuwien.ac.at/forth/gforth/Snapshots/current/gforth.tar.xz
tar xf gforth.tar.xz
```

3) Перейдите в каталог:
```
cd gforth-*
```

4) Скрипт install-deps.sh может не работать на OpenSUSE, поэтому пропустите его.

5) Продолжайте:

```
./configure
```

6) Скомпилируйте:
```
make
```

7) Установите:
```
sudo make install
```

8) Возможные проблемы и решения
* **Если при сборке или установке возникают ошибки:**
  * Проверьте зависимости: возможно, нужны дополнительные библиотеки
  * Если ошибка в ./configure, проверьте вывод и установите недостающие пакеты через zypper
  * Для конкретных ошибок нужно смотреть логи, которые выводятся в терминал
  * Если вы столкнулись с конкретной ошибкой, укажите её текст, и я смогу помочь точнее.