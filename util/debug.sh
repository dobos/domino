#!/bin/bash
rm -f debug.log
rm -f stdout.log
rm -f stderr.log

# build script
GDB_CMD="
set logging file debug.log
set logging on
set logging redirect on

file $1
start ${@:2} > stdout.log 2> stderr.log

info inferior
continue

if \$_isvoid (\$_exitcode)
  bt
end

if \$_isvoid (\$_exitcode)
  call fflush(0)
  kill
end

quit
"

echo "$GDB_CMD" > gdb.cmd

# run gdb and wait for completion
gdb --batch -x gdb.cmd &
GDB_PID=$!

timeout 3 tail --pid=$GDB_PID -f /dev/null

# if still running, kill inferior process
if [ -d /proc/$GDB_PID ]
then
  INF_PID=$(grep -oP "process \K([0-9]+)" debug.log)
  if [ -d /proc/$INF_PID ]
  then
    kill -s 2 $INF_PID
  fi
fi
