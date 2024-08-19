#!/usr/bin/env bash

# This script will find and publish a NuGet package to NuGet.org

# Requirements:
# - NuGet is installed on the machine and is accessible everywhere (added to PATH)

# Parse command line arguments
package="$1"
apiKey="$2"

nuget push "$package" -src https://api.nuget.org/v3/index.json -ApiKey "$apiKey"

exit 0