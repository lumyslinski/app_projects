#!/bin/bash

gnome-terminal -x bash ./RunApi.sh & sleep 5
gnome-terminal -x bash ./RunEndpoint.sh & sleep 5
gnome-terminal -x bash ./RunWorker.sh & sleep 5
gnome-terminal -x bash ./RunTests.sh