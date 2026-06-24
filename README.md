# 🧮 String Calculator Challenge (C#)

Implementación del clásico kata **String Calculator** en C# / .NET, resuelto de forma **incremental y con TDD**: cada requisito se agrega como un paso con sus tests y su commit.

La función principal recibe un `string` con números y separadores y devuelve la suma:

```csharp
Add("1,2,3"); // => 6
```

## ✨ Requisitos implementados

| Paso | Requisito | Ejemplo |
|---|---|---|
| 1 | Máx. 2 números separados por coma; vacíos/inválidos → `0`; >2 lanza excepción | `"1,5000"` → `5001` · `"2,"` → `2` |
| 2 | Cantidad ilimitada de números | `"1,2,3,...,12"` → `78` |
| 3 | Soporte de `\n` como separador alternativo | `"1\n2,3"` → `6` |
| 4 | Prohibir negativos (la excepción lista **todos** los negativos) | `"1,-2,3,-5"` → excepción con `-2, -5` |
| 5 | Números > 1000 se ignoran (cuentan como `0`) | `"2,1001,6"` → `8` |
| 6 | Delimitador custom de 1 char: `//{d}\n{nums}` | `"//#\n2#5"` → `7` |
| 7 | Delimitador custom de cualquier longitud: `//[{delim}]\n{nums}` | `"//[***]\n11***22***33"` → `66` |
| 8 | Múltiples delimitadores: `//[{d1}][{d2}]...\n{nums}` | `"//[*][!!][r9r]\n11r9r22*hh*33!!44"` → `110` |

## 🏗️ Diseño

Separación de responsabilidades por capa:

- **Parser / tokenizer** — interpreta los delimitadores (coma, `\n`, custom).
- **Normalización** — tokens faltantes o inválidos se convierten en `0`.
- **Reglas** — validación de negativos y cota superior (`> 1000`).
- **Sumatoria** — cálculo del resultado final.

## 🧪 Tests

El proyecto incluye un set de **unit tests** que cubre cada paso (enfoque TDD).

```bash
dotnet test
```

## ▶️ Ejecución

```bash
dotnet run --project "Calculator Challenge"
```

## 🛠️ Stack

- **C# / .NET**
- Proyecto de consola + proyecto de tests (`Calculator Challenge.Tests`)

---

> Resuelto por [Franco Sosa](https://github.com/francososa97) como challenge técnico. El enunciado original completo está en [`readmee.txt`](./readmee.txt).
