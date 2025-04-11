IFB-LIB — Библиотека для оптимизации графики
===
[Eng](./README.md) | [Rus](./README-RUS.md)
**C# / C++** кроссплатформенная библиотека для оптимизации 3D-графики.
***

## Оглавление

- [Возможности](#возможности)
- [Демонстрация](#демонстрация)
- [Установка](#установка)
- [Обзор проектов](#обзор-проектов)
    - [C++ библиотека](#cpp-lib)
    - [C# библиотека](#cs-lib)
    - [Unity-пакет](#unity-package)

***

## Возможности

- **Кроссплатформенность**
    - > [Android arm64](https://github.com/intfloatbool/ifb-lib-optimization/releases/download/v1.0/ifb-lib-unity-sample-build-android-arm64.zip)
    - > [Windows x64](https://github.com/intfloatbool/ifb-lib-optimization/releases/download/v1.0/ifb-lib-unity-sample-build-win64.zip)

<br>

- **Потокобезопасность** — `ConcurrentManagedMesh`, `ConcurrentFastUnityMesh`
- **Простота API** — `SimplifyMeshFast(...)`, `SimplifyMeshFastAsync(...)`, `MonoMeshSimplifier`
- **Быстрая симплификация мешей в рантайме** — на основе алгоритмов [Zeux](https://github.com/zeux/meshoptimizer)
- **Отсутствие зависимостей при сборке**
- **[Поддержка Unity](https://github.com/intfloatbool/ifb-lib-optimization/releases/download/v1.0/ifb-lib-unity-package.unitypackage) и других C# приложений для работы с 3D**

***

## Демонстрация

**Исходная сцена**  
![Исходная сцена](/images/default_192k_tris.png)

**После симплификации**  
![Применение симплификации](/images/result.png)

> | Тип     | Треугольники |
> |----------|--------------|
> | NONE     | 191.8k       |
> | LOW      | 124.0k       |
> | MEDIUM   | 88.4k        |
> | HIGH     | 66.9k        |

**Пример отображения на устройстве**

> **LOW**  
> ![LOW](/images/device_0_low.png)

> **MEDIUM**  
> ![MEDIUM](/images/device_1_medium.png)

> **HIGH**  
> ![HIGH](/images/device_2_high.png)

***

## Установка

> **Unity**
> - Требуется [UniTask](https://github.com/Cysharp/UniTask)
> - Установите [ifb-lib Unity package](https://github.com/intfloatbool/ifb-lib-optimization/releases/download/v1.0/ifb-lib-unity-package.unitypackage)

<br>

> **Другие C# приложения**
> - Скачайте и подключите [ifblib .NET библиотеку](https://github.com/intfloatbool/ifb-lib-optimization/releases/download/v1.0/ifb-lib-binaries.zip). Используйте её API.
> - Для работы .NET-библиотеки требуется C++-библиотека. Скачайте и разместите [ifblib C++ библиотеку](https://github.com/intfloatbool/ifb-lib-optimization/releases/download/v1.0/ifb-lib-binaries.zip) рядом с вашим исполняемым файлом.

***

## Обзор проектов

---

### C++ библиотека  
> [/ifb-lib-cpp/](./ifb-lib-cpp/)

### C# библиотека  
> [/ifb-lib-net/](./ifb-lib-net/)

### Unity-пакет  
> [/ifb-lib-unity/](./ifb-lib-unity/)
