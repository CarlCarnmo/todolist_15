# todolist_CC

- [x] Look over the code.
- [x] Turn menu from IF statements into a switch. [#2520693](https://github.com/CarlCarnmo/todolist_15/commit/25206935b1624195f239ba9c8b457dbbef3f3441)
- [x] Rewrite the Print methods. To make implementation of adaptable Print later easier and less messy.
- [x] Add list and describe commands with substrings
```
case String A when A.StartsWith("list"):
Todo.Print(command);
...

string[] arg = command.Split(' ');
```
- [x] Add Methods for wanted commands one by one:
```
setStatus(string command)
saveTodo(string command)
loadTodo(string command)
copyTodo(string command)
editTodo(string command)
newTodo(string command)
PrintHelp(string command)
```
- [x] Upgrade Print to adjust depending on the length of 'item.task' and 'item.taskDescription'. Had to add another Method 'getCount()' with double return values using Tuple to solve this.
- [x] Limit length of 'item.task' and 'item.taskDescription' to prevent ugly Prints and/or crashes.
- [x] Finished 1.0. Patching "bugs" and ugly code etc.
- [ ] Patching things and stuff.
