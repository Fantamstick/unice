## What is Unice?
Unice is a light framework and a concept for building a Unity3D game that is highly data-driven and view-centric testable. It's not yet battle-tested and is still a WIP.

## Why Unice?
### Rapid Test Iteration
Most projects have tightly-coupled code which discourages fast iteration on the individual pieces of some logic or scene. For small projects, this is not a problem since you can replay parts of a scene or condition somewhat painfree, but for large projects it becomes tedius when
- Dealing with corner case conditions
- Making small tweaks
- Debugging to make sure nothing breaks after code changes. 

### Data-driven
Prefabs allow values to propagate across different scenes and allow reusability, but it's limited to gameobjects. Unice makes use of ScriptableObjects to reuse not just data, but also *Services*. Services are drag-and-drop scriptable objects used to give components access to behaviours outside of its immediate scope.

## Requirements
- Unity 2019.3+
- [UniTask](https://github.com/Cysharp/UniTask) 1.3.1

## Using Unice
Unice is made up of the following concepts:
- Director (DIR)
- Container
  - View (VI)
  - Controller (CO)
- Service
- Data Object (DO)

In a nutshell, directors tell the game *when* to run logic, containers tell the game *how* to run logic, and services help containers use asset and game data *without tight coupling*.

![Hierarchy](/StoreDocument/hierarchy.jpg?raw=true "Hierarchy")

### Director (DIR)
A director dictates *when* and *with what* controllers run. At the most macro-level, coordinators control the flow of the entire game and at the most micro can control the flow of the state of a scene such as a pause menu.

It instantiates containers to do things and lets them tell the director relevant data.

![Director Flow](/StoreDocument/director_flow.jpg?raw=true "Director Flow")

### Container
A container can be almost anything that can exist by itself in a Scene that interacts with the player in some way or fashion such as a button, image, collision, input, animation, etc. A container may not even have a view such as a random number generator.

Containers are created by directors and are sometimes given data for them to do work. Directors do not know how coordinators work, they only know when they should be working.

#### View (VI)
A container's view is simply the glue that holds the container together. It has little logic  and is always a MonoBehaviour so it can exist in a Scene. It simply exists to 
- Interact with scene.
- Allow the developer to set important data via the inspector.

#### Controller (CO)
The container's controller takes control of the view's data and references for its processes. It communicates with a coordinator if needed.

### Data Object (DO)
A data object is a persistant ScriptableObject asset that lives in the project. It is useful for assigning test and production data to containers.

## Testing

### View

![View Testing](/StoreDocument/view_testing.jpg?raw=true "View Testing")

### Controller

![Controller Testing](/StoreDocument/controller_testing.jpg?raw=true "Controller Testing")

## Installation as UMP Package
Add `https://github.com/Fantamstick/unice.git?path=Assets/Plugins/Unice/Scripts` to Package Manager.

or locate `manifest.json` in your Unity project's `Packages` folder and add the following dependencies:
```
"dependencies": {
  "com.fantamstick.unice": "https://github.com/Fantamstick/unice.git?path=Assets/Plugins/Unice/Scripts",
  ...
}
```

## References
Motivation was born out of a desire to create more testable and highly iterative Unity projects. Many great ideas came from Ryan Hipple and his [blog post](http://www.roboryantron.com/2017/10/unite-2017-game-architecture-with.html) and video at Unite 2017 titled [Game Architecture with Scriptable Objects](https://www.youtube.com/watch?v=raQ3iHhE_Kk) and from Yoshifumi Kawai (a.k.a. neuecc) the creator of [UniRx](https://github.com/neuecc/UniRx) and [UniTask](https://github.com/Cysharp/UniTask).

## License
This library is under the MIT License.