## Guns

Hi, In this file we will be looking at multiple guns scripts.

* The first Gun.cs Script contains the main Gun animation code so this script connects all of the other scripts and does firing, reload, and more animations.
```cs script
// All of the variables are edited by you in the editor, just test until you find something good.
float Sway_Speed;
float Sway_Smooth;
public bool isAiming = false;
public bool Aiming = false;
public float Aim_Speed;
public Sway Sway_Script;
public Animator Animator;
public Vector3 Aim_Position;
public GameObject Crosshair;
private Vector3 Initial_Position; 
public float Damage = 10f;
public float Range = 100f;
public float Mag = 30;
public float totalMag = 180;
public Camera Camera;  
```
* The Sway.cs script adds this moving effect to the gun so whenever you move the camera the gun won't stay static.
```cs script
// All of the variables are edited by you in the editor, just test until you find something good.
public float Amount;
public float Smooth_Amount;
public Vector3 Initial_Position;
```
* The Aim.cs script is just a simple script that updates the Gun Object position when clicked the right mouse button.
```cs script
// Change the position up to your gun centering point.
public Vector3 Aim_Position;
public float Aim_Speed = 5;
bool isAiming = false;
bool Aiming = false;
```

---
<div align=center>
  <img src="https://forthebadge.com/images/badges/made-with-c-plus-plus.svg" />
</div>
