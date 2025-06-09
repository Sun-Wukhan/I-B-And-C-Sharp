#!/bin/bash

# Set the server address
SERVER="localhost:5205"
PROTO_FILE="FF7Game.Server/Protos/game.proto"

# List all available services
echo "Listing available services:"
grpcurl -plaintext -proto $PROTO_FILE $SERVER list

echo -e "\nListing methods in GameService:"
grpcurl -plaintext -proto $PROTO_FILE $SERVER list ff7game.GameService

# Test GetCharacter for Cloud
echo -e "\nTesting GetCharacter for Cloud:"
grpcurl -plaintext -proto $PROTO_FILE -d '{"character_name": "Cloud"}' $SERVER ff7game.GameService/GetCharacter

# Test GetCharacter for Tifa
echo -e "\nTesting GetCharacter for Tifa:"
grpcurl -plaintext -proto $PROTO_FILE -d '{"character_name": "Tifa"}' $SERVER ff7game.GameService/GetCharacter

# Test StartBattle
echo -e "\nTesting StartBattle with Cloud vs Sephiroth:"
grpcurl -plaintext -proto $PROTO_FILE -d '{"character_name": "Cloud", "enemy_name": "Sephiroth"}' $SERVER ff7game.GameService/StartBattle

# Test LevelUp
echo -e "\nTesting LevelUp for Cloud:"
grpcurl -plaintext -proto $PROTO_FILE -d '{"character_name": "Cloud"}' $SERVER ff7game.GameService/LevelUp

# Test GetCharacter again to see the level up effect
echo -e "\nTesting GetCharacter for Cloud after level up:"
grpcurl -plaintext -proto $PROTO_FILE -d '{"character_name": "Cloud"}' $SERVER ff7game.GameService/GetCharacter 