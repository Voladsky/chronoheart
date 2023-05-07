# Chronoheart
![main-menu](https://user-images.githubusercontent.com/59023506/229347046-0618c414-57d4-4106-b0fd-588fa723e308.png)

Chronoheart – платформер с элементами ритм-игры и слэшера.

Герою предстоит подняться по этажам Великой Башни, населенной механизмами, попутно решая головоломки и побеждая разнообразных боссов. 

## Как пройти игру с первой попытки

### Основное управление
| Действие  | Кнопка |
| --- | --- |
| Движение  | `A`, `D` |
| Взаимодействие | `E` | 
| Ближний удар | `ЛКМ`/`K` |
| Дальний удар | `ПКМ`/`L` |
| Рывок (доступен начиная с уровня 2)  | `Shift` |

Рывок доступен также в прыжке.

В правом нижнем углу находится метроном. Это - счётчик тиков, ритм-элемент игры. Когда он горит желтым, это означает, что в этот момент можно совершать комбо атаки. 

Комбо ОБЯЗАТЕЛЬНО совершать в один тик (когда желтая обводка пропадает, накопленные нажатия сбрасываются). Комбинации выполняются быстрыми ПОСЛЕДОВАТЕЛЬНЫМИ нажатиями клавиш. На данный момент реализовано 5 комбинаций (для реализации комбо сначала нажимается первая, затем вторая клавиша):
- `ЛКМ`/`K` + `ПКМ`/`L` - дальняя атака, но запускаемый объект быстрее и наносит больше урона
- `W` + `ЛКМ`/`K` - ближняя атака, подбрасывает противников перед игроком вверх
- `S` + `ЛКМ`/`K` - ближняя атака, толкает противников перед игроком в сторону
- `Space` + `ЛКМ`/`K` - ближняя атака, когда игрок касается земли, наносит урон противникам слева и справа от игрока

Здоровья у игрока нет, но есть оставшееся время в левом верхнем углу экрана. Нанося урон, враги уменьшают оставшееся время.

На уровне присутствуют точки сохранения в виде часов. Взаимодействовать с ними можно на `E` (должно вывестись **Saved!** в левом нижнем углу экрана). После смерти или при нажатии **Continue** в главном меню или **Restart** в меню паузы игрок возродится на этой точке.

Взаимодействие с рычагами тоже осуществляется на клавишу `E`.

На уровнях встречаются бочки и кувшины. Их можно разбивать атаками.

Для простоты понимания комбо-атак ниже приложена краткая демонстрация всех возможностей атаки для зачистки одной из арен первого уровня:



https://user-images.githubusercontent.com/59023506/229347766-4d29d769-ebc6-4aa2-8615-89aea9a6b490.mp4

