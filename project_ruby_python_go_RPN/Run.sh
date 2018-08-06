#!/bin/bash

gnome-terminal -- bash ./RunApp.sh & sleep 5
gnome-terminal -- bash ./RunTests.sh  & sleep 5
gnome-terminal -- bash ./RunLogs.sh  & sleep 5
gnome-terminal -- bash ./RunEndpoint.sh