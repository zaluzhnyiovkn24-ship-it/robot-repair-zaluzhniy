# Схема архітектури зрізу

Домашнє до лекції 11. Схема лежить поруч: [architecture.png](architecture.png). Нижче п'ять кроків, з яких вона зібрана.

1. **Об'єкти рівня:** робот (PlayerCharacter), сторожові (Enemy), аптечки (CollectibleHealth), зони шкоди (DamageZone), NPC, снаряд (Projectile), GameManager, UIDocument з HUD, камера, тайлмап і декорації.
2. **Компоненти:** кожен об'єкт складається з компонентів Unity (Rigidbody2D, колайдер, Animator, SpriteRenderer) і одного власного скрипта: PlayerController, EnemyController, HealthCollectible, DamageZone, NonPlayerCharacter, Projectile, GameManager, UIHandler.
3. **Префаби:** Enemy повторюється на рівні, а його варіант EnemyFast відрізняється лише числами (Speed, Change Time) і кольором. Аптечка теж має варіант CollectibleHealthBig з іншим Amount. Об'єкти, що відрізняються лише числами, це варіанти одного префаба, а не два окремі.
4. **Зв'язки:** сторожові, зона шкоди й аптечка викликають `PlayerController.ChangeHealth`; робот повідомляє HUD через синглтон `UIHandler.instance`; снаряд викликає `EnemyController.Fix()`, який повідомляє HUD про лічильник; GameManager читає здоров'я робота й `isBroken` ворогів, а вороги знаходяться через `FindObjectsByType`. Камера просто стежить за роботом (Follow).
5. **Числа tuning table:**

| Число | Значення | Де живе |
|---|---|---|
| Швидкість робота | 3,5 | поле `speed` префаба PlayerCharacter |
| Здоров'я робота | 5 | поле `maxHealth` префаба PlayerCharacter |
| Невразливість | 1,5 с | поле `timeInvincible` префаба PlayerCharacter |
| Пауза між пострілами | 0,5 с | поле `launchCooldown` префаба PlayerCharacter |
| Сила кидка снаряда | 300 | було в коді, тепер поле `projectileForce` у PlayerCharacter |
| Життя снаряда | 2 с | поле `lifeTime` префаба Projectile |
| Швидкість і час розвороту сторожового | 1 і 3 | поля Enemy; у варіанті EnemyFast переписано на 2 і 1,5 |
| Лікування аптечки | 1 | поле `amount` префаба CollectibleHealth (варіант Big: 2) |
| Шкода зони | 1 | поле `damage` префаба DamageZone |
| Час показу репліки | 4 с | поле `displayTime` в UIHandler |
| Пауза перед перезапуском | 3 с | поле `restartDelay` у GameManager |

## Перевірка себе

- Найбільше стрілок веде до **PlayerController**: у нього б'ють сторожові, зона й аптечка, його читає GameManager, за ним їде камера. Зміна сигнатури `ChangeHealth` зачепить усіх, хто на нього веде.
- Кожне число tuning table має своє місце в таблиці вище.
- Два об'єкти, що відрізняються лише числами (Enemy і EnemyFast), зроблені варіантом.
- Подія замість прямого виклику: зараз робот напряму викликає `UIHandler.SetHealthValue`. Краще було б подією «здоров'я змінилося», на яку підпишуться смуга, звук і камера. У цьому зрізі лишаю прямий виклик, а подію відкладаю на юніт 6.
