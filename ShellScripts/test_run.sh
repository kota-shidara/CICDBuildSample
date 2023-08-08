#!/bin/zsh

text="$1"

touch "/Users/kota/Desktop/tst.txt" 
echo "This is a test. ${text} " > "/Users/kota/Desktop/tst.txt"
echo "This is a test!!!!!!. ${text}"
touch "/Users/kota/Desktop/tst${text}.txt"