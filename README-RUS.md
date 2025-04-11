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
    - > [Android arm64](https://ссылка_на_android_build)
    - > [Windows x64](https://ссылка_на_win64_build)

<br>

- **Потокобезопасность** — `ConcurrentManagedMesh`, `ConcurrentFastUnityMesh`
- **Простота API** — `SimplifyMeshFast(...)`, `SimplifyMeshFastAsync(...)`, `MonoMeshSimplifier`
- **Быстрая симплификация мешей в рантайме** — на основе алгоритмов [Zeux](https://github.com/zeux/meshoptimizer)
- **Отсутствие зависимостей при сборке**
- **[Поддержка Unity](https://ссылка_на_unity_package) и других C# приложений для работы с 3D**

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
> - Установите [ifb-lib Unity package](https://ссылка_На_unity_package)

<br>

> **Другие C# приложения**
> - Скачайте и подключите [ifblib .NET библиотеку](https://ссылка_на_cs_сборку). Используйте её API.
> - Для работы .NET-библиотеки требуется C++-библиотека. Скачайте и разместите [ifblib C++ библиотеку](https://ссылка_на_cpp_сборку) рядом с вашим исполняемым файлом.

***

## Обзор проектов

---

### C++ библиотека  
> [/ifb-lib-cpp/](./ifb-lib-cpp/)

### C# библиотека  
> [/ifb-lib-net/](./ifb-lib-net/)

### Unity-пакет  
> [/ifb-lib-unity/](./ifb-lib-unity/)
