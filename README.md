IFB-LIB — Graphics Optimization Library  
===

[Eng](./README.md) | [Rus](./README-RUS.md)


**C# / C++** cross-platform library for real-time 3D mesh optimization.

***

## Table of Contents

- [Features](#features)
- [Demo](#demo)
- [Installation](#installation)
- [Project Overview](#project-overview)
    - [C++ Library](#cpp-lib)
    - [C# Library](#cs-lib)
    - [Unity Package](#unity-package)

***

## Features

- **Cross-platform**
    - > [Android arm64](https://github.com/intfloatbool/ifb-lib-optimization/releases/download/v1.0/ifb-lib-unity-sample-build-android-arm64.zip)
    - > [Windows x64](https://github.com/intfloatbool/ifb-lib-optimization/releases/download/v1.0/ifb-lib-unity-sample-build-win64.zip)

<br>

- **Thread-safe** — `ConcurrentManagedMesh`, `ConcurrentFastUnityMesh`
- **Simple API** — `SimplifyMeshFast(...)`, `SimplifyMeshFastAsync(...)`, `MonoMeshSimplifier`
- **Fast real-time mesh simplification** — powered by [Zeux's algorithm](https://github.com/zeux/meshoptimizer)
- **No external dependencies required for build**
- **[Unity support](https://github.com/intfloatbool/ifb-lib-optimization/releases/download/v1.0/ifb-lib-unity-package.unitypackage)** and compatibility with other C# 3D applications

***

## Demo

**Original Scene**  
![Original Scene](/images/default_192k_tris.png)

**After Simplification**  
![Simplified Scene](/images/result.png)

> | Mode   | Triangles   |
> |--------|-------------|
> | NONE   | 191.8k      |
> | LOW    | 124.0k      |
> | MEDIUM | 88.4k       |
> | HIGH   | 66.9k       |

**Device Preview**

> **LOW**  
> ![LOW](/images/device_0_low.png)

> **MEDIUM**  
> ![MEDIUM](/images/device_1_medium.png)

> **HIGH**  
> ![HIGH](/images/device_2_high.png)

***

## Installation

> **Unity**
> - Requires [UniTask](https://github.com/Cysharp/UniTask)
> - Install the [ifb-lib Unity package](https://github.com/intfloatbool/ifb-lib-optimization/releases/download/v1.0/ifb-lib-unity-package.unitypackage)

<br>

> **Other C# Applications**
> - Download and reference the [ifblib .NET library](https://github.com/intfloatbool/ifb-lib-optimization/releases/download/v1.0/ifb-lib-binaries.zip). Use its API.
> - The .NET library requires the native C++ library. Download and place the [ifblib C++ build](https://github.com/intfloatbool/ifb-lib-optimization/releases/download/v1.0/ifb-lib-binaries.zip) next to your executable.

***

## Project Overview

---

### C++ Library  
> [/ifb-lib-cpp/](./ifb-lib-cpp/)

### C# Library  
> [/ifb-lib-net/](./ifb-lib-net/)

### Unity Package  
> [/ifb-lib-unity/](./ifb-lib-unity/)
