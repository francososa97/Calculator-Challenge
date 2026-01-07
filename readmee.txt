Tenés que hacer una **“String Calculator”**: una app de consola que recibe **un string** con números y separadores, y **devuelve la suma**. La gracia del challenge no es la suma, sino **ir agregando requisitos de a uno**, con **tests** y **un commit por requisito**.

## Qué te piden entregar

* **Repositorio público (GitHub)** nuevo (no fork).
* **Consola** (CLI) en el lenguaje que te dijo tu entrevistador.
* **Unit tests** (clave).
* **Cada “step” como un commit separado** (pensalo como “cambio de requerimiento” incremental).
* Prioridad: **legibilidad + separación de responsabilidades** más que micro-performance.

---

## Qué hace tu función principal

Algo tipo:

`Add(string input) -> int`

Le pasás `"1,2,3"` y te devuelve `6`.

---

## Requisitos (paso a paso) en español

### 1) Máximo 2 números con coma

* Input: `"20"` → `20`
* Input: `"1,5000"` → `5001`
* Input: `"4,-3"` → `1` *(ojo: en este paso todavía se permiten negativos; recién se prohíben en el paso 4)*
* **Si hay más de 2 números**: tirar excepción.
* **Vacío o faltantes** se vuelven `0`:

  * `""` → `0`
  * `"2,"` → `2` (porque el segundo es “missing” => 0)
  * `",3"` → `3`
* **Inválidos** se vuelven `0`:

  * `"5,tytyt"` → `5`

👉 En este paso la app es muy permisiva: si no parsea, es 0; si falta, es 0.

---

### 2) Ya no hay máximo de cantidad

* `"1,2,3,4,5,6,7,8,9,10,11,12"` → `78`
* O sea: ahora sumás **N números** separados por coma.

---

### 3) Soportar `\n` como delimitador alternativo

* `"1\n2,3"` → `6`
* Ahora los separadores válidos: **`,` y `\n`** (ambos a la vez).

---

### 4) Prohibir negativos

* Si hay algún negativo: **tirar excepción** y el mensaje debe incluir **todos** los negativos encontrados.

  * `"1,-2,3,-5"` → excepción que incluya `-2` y `-5`
* Ojo: no es “falla en el primero”; te piden **recolectar todos** y reportarlos.

---

### 5) > 1000 es inválido (se toma como 0)

* `"2,1001,6"` → `8`
* (1001 no suma)
* 1000 sí suma, 1001 no.

---

### 6) Delimitador custom de 1 char con formato `//{d}\n{numbers}`

* `"//#\n2#5"` → `7`
* `"//,\n2,ff,100"` → `102` *(“ff” inválido => 0)*
* Importante: **todo lo anterior sigue funcionando** (coma, newline, negativos, >1000, etc.).

---

### 7) Delimitador custom de cualquier longitud `//[{delim}]\n{numbers}`

* `"//[***]\n11***22***33"` → `66`
* También mantiene compatibilidad con lo anterior.

---

### 8) Múltiples delimitadores de cualquier longitud

Formato:
`//[{d1}][{d2}]...\n{numbers}`

Ejemplo:

* `"//[*][!!][r9r]\n11r9r22*hh*33!!44"` → `110`

  * acá: `*`, `!!`, `r9r` son delimitadores
  * `hh` es inválido => 0
  * suma: 11 + 22 + 0 + 33 + 44 = 110

---

## Stretch goals (opcionales)

1. Mostrar la “fórmula” usada:

* `"2,,4,rrrr,1001,6"` → `2+0+4+0+0+6 = 12`

2. Modo interactivo: seguir leyendo inputs hasta Ctrl+C.

3. Argumentos CLI para configurar:

* delimitador alternativo del paso 3
* toggle para permitir/prohibir negativos
* upper bound (1000)

4. Usar **DI** (inyección de dependencias).

5. Agregar operaciones: resta/multi/div.

---

## Cómo se evalúa (lo que suelen mirar)

* **Tests por cada paso** (ideal: TDD).
* **Commits limpios**: “Step 1”, “Step 2”… con cambios acotados.
* Diseño fácil de leer:

  * Parser/tokenizer (delimitadores)
  * Normalización de tokens (missing/invalid)
  * Reglas (negativos, >1000)
  * Sumatoria / resultado
* Mensajes de excepción claros.

---

## Plan de commits recomendado (para que te quede prolijo)

1. Commit: proyecto consola + framework de tests + primer test “empty => 0”.
2. Step 1 completo: 2 números, coma, invalid/missing => 0, excepción si >2.
3. Step 2: N números.
4. Step 3: soportar `\n`.
5. Step 4: excepción con todos los negativos.
6. Step 5: ignorar >1000.
7. Step 6: custom delimiter 1 char (`//x\n`).
8. Step 7: custom delimiter largo (`//[***]\n`).
9. Step 8: múltiples delimitadores (`//[*][!!]\n`).
10. (Opcional) Stretch goals.

---

Si me decís **en qué lenguaje te lo pidieron** (C#, Java, JS/TS, Python, etc.), te armo:

* estructura de carpetas,
* set de tests mínimo por step,
* y una guía de commits con nombres concretos (y ejemplos de casos borde).
