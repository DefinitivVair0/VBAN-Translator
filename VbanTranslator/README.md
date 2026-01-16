# VBAN Translator
> Note that this application does not have extensive error checking/handling so when something crashes or does not output anything you should check your input.

This app is designed to take in VBAN command(s) and translate it to ASCII while prepending the header.

This way every application that can send text over UDP can send VBAN commands.

The only limitation is that some parts of the VBAN protocoll require bytes between 0x00 and 0x19, which don't translate to any alphanumeric character or other symbol in ASCII.
This means that the sending application has to either:

- be able to read and send non-standart characters (looks something like this: )

or

- be able to distinguish standart and non-standart characters by utilizing \ (e.g. 0x12 can be represented as \12)

If the application can do neither, it won't be possible to send VBAN messages with this method at this point in time.

## "Installation"
At the moment there is no insaller. You have to download the .zip from Releases, extract it and run the .exe inside.
The .exe has to be run inside the folder with the other files.
If not already installed, you will be promtped to download the .NET 10 Desktop Runtime.

## How to use
1. Insert your command(s) into the "Command" text box (don't use newlines for multiple commands, write them inline and seperated by ";")
1. Select mode using the check box
1. Insert your VBAN-TEXT name (when using pure ASCII mode this has to be exactly 16 characters long)
1. Click "Translate" (this will copy the output into your clipboard)
