#!/bin/bash

gnome-terminal -x bash ./RunApp.sh & sleep 5
gnome-terminal -x bash ./RunTests.sh  & sleep 5
gnome-terminal -x bash ./RunLogs.sh  & sleep 5
gnome-terminal -x bash ./RunEndpoint.sh