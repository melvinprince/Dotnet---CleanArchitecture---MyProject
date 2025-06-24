#!/usr/bin/env bash
set -euo pipefail

# Ensure required packages for adding repositories
sudo apt-get update
sudo apt-get install -y wget gnupg software-properties-common apt-transport-https

# Install .NET SDK as specified in global.json
DOTNET_VERSION=$(jq -r '.sdk.version' global.json)
DOTNET_INSTALL_DIR=/usr/share/dotnet
wget -q https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh -O /tmp/dotnet-install.sh
sudo bash /tmp/dotnet-install.sh --version "$DOTNET_VERSION" --install-dir "$DOTNET_INSTALL_DIR"

# Add dotnet to PATH for current session
export PATH="$DOTNET_INSTALL_DIR:$PATH"

# Install Node.js and npm from Ubuntu repositories
sudo apt-get install -y nodejs npm

# Optionally install Playwright browsers (may be large)
# npx playwright install --with-deps

cat <<MSG
Setup completed. dotnet version: $(dotnet --version)
node version: $(node --version)
MSG
