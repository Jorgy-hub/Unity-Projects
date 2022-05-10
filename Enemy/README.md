## Enemies

In this folder you will be looking at multiple enemies files.
I've just added the basic AI follower at the moment with attacking to Health script in the player folder.

* EnemyAI.cs
```cs script
// Nav Mesh Agent is needed for movement.
public NavMeshAgent Mob;
// Tarjet to follow.
public GameObject Player;
// Place to return when user is not on field anymore.
public GameObject Spawn;
// Animator for controlling the animations.
public Animator Animator;
// This is the running speed I recommend 4.0f
public float Mob_Run = 4.0f;
// Extra Speed float, keep it on 1f
public float Speed = 1f;
// Health Script of the Player.
public Health Health;
// Damage done per attack, you decide this
public float Damage = 20;
// Check collision, don't touch
bool Colliding = false;
```

There's just a few stuff on Enemy cause I haven't needed a lot but you can suggest more stuff for me to add!

---
<div align=center>
  <img src="https://forthebadge.com/images/badges/built-with-love.svg" />
  <img src="https://forthebadge.com/images/badges/made-with-c-sharp.svg" />
  <img src="https://forthebadge.com/images/badges/powered-by-qt.svg" />
</div>
