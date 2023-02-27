# expression-evaluator
An expression calculator evaluating commands sequentially either in integer or double-precision floating-point arithmetic.

The program processes commands from standard input and prints results (or error messages) to standard output. Commands are processed sequentially.

The calculator recognizes the following commands:

1. A line starting with the `=` symbol followed by one space and an expression in preorder format (see below). Such command will parse the expression and store it; following operations will be done over the last parsed expression. If an expression was already stored, the previous expression will be discarded and replaced by the new expression. The previous expression will be discarded even if an error was encountered when processing the `=` command.
2. A single string `i` will evaluate the last expression using integer arithmetic and print out the result.
3. A single string `d` will evaluate the last expression using double-precision floating-point arithmetic (64 bits) and print out the result using 5 decimal places.

The following operations are supported: binary operators `+`, `-`, `*` and `/`, as well as the unary minus operator `~` representing additive inverse.

**An example of program usage:**
```
= + 2 3
i
5
d
5.00000
= / * 11 22 * 4 ~ 5
i
-12
d
-12.10000
= * 3
Format Error
```
