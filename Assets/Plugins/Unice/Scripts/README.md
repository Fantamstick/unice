## What is Unice?
Unice is a light framework and way of building up a Unity3D project that is highly data-driven and allows developers to create a highly sustainable and view-centric testable environment. It's not yet battle-tested and a work in progress.

## Why Unice?
### Rapid Test Iteration
Most projects have tightly-coupled code which discourages fast iteration on the individual pieces of some logic or scene. For small projects, this is not a problem since you can replay parts of a scene or condition somewhat painfree, but for large projects it becomes tedius when
- Dealing with corner case conditions
- Making small tweaks
- Debugging to make sure nothing breaks after code changes. 

### Data-driven
Prefabs allow values to propagate across different scenes and allow reusability, but it's limited to gameobjects. Unice makes use of ScriptableObjects to reuse not just data, but also *Services*. Services are drag-and-drop scriptable objects used to give components access to behaviours outside of its immediate scope.

## Requirements
- Unity 2019.2 verified (but should work with Unity 2018.3 or later)
- [UniTask](https://github.com/Cysharp/UniTask) 1.2.0

## Installation
Locate `manifest.json` in your Unity project's `Packages` folder and add the following dependencies:
```
"dependencies": {
  "com.fantamstick.unice": "https://github.com/Fantamstick/unice.git",
  "com.unitask": "https://github.com/futsuki/unitask-package.git",
  ...
}
```

### Updating
Delete Unice's "lock" field in `manifest.json` to allow the Unity editor find and update the framework to the latest version.
```"lock": {
    "com.fantamstick.unice": {
      "hash": "b601a6b894a14613de804317d2d1e408cfa70a79",
      "revision": "HEAD"
    }
    ...
  }
```

## Using Unice
Unice is made up of the following assets:
- Coordinator
- Container
  - View (MB)
  - Controller (CO)
- Service
- Data Object (SO)

### Coordinator
A game is composed of coordinators which dictate when processes run. A coordinator could be in the context of a Scene or even more granular such as a pause menu.

### Container
A container can be anything that can exist by itself in a Scene that interacts with the player in some way or fashion such as a button, image, collision, input, animation, etc.

[TODO: Post image here]

#### View (MB)
The container's (MonoBehaviour) view is simply the glue that holds the container together. It has no logic at all. It simply exists to 
- instantiate its Controller 
- Allow the developer to define important data via the inspector.

#### Controller (CO)
The container's controller takes control of the view's data and references for its processes. 

## References
Motivation was born out of a desire to create more testable and highly iterative Unity projects. Many great ideas came from Ryan Hipple and his [blog post](http://www.roboryantron.com/2017/10/unite-2017-game-architecture-with.html) and video at Unite 2017 titled [Game Architecture with Scriptable Objects](https://www.youtube.com/watch?v=raQ3iHhE_Kk) and from Yoshifumi Kawai (a.k.a. neuecc) the creator of [UniRx](https://github.com/neuecc/UniRx) and [UniTask](https://github.com/Cysharp/UniTask).

## License
This library is under the MIT License.