const tmi = require('tmi.js');

let lapin = 47;

// Define configuration options
const opts = {
    identity: {
        username: "//USERNAME",
        password: "//API KEY"
    },
    channels: [
        "happylewie"
    ]
};

// Create a client with our options
const client = new tmi.client(opts);

// Register our event handlers (defined below)
client.on('message', onMessageHandler);
client.on('connected', onConnectedHandler);

// Connect to Twitch:
client.connect();

// Called every time a message comes in
function onMessageHandler(target, context, msg, self) {
    if (self) {
        return;
    } // Ignore messages from the bot

    // Remove whitespace from chat message
    const commandName = msg.trim();

    if (commandName.charAt(0) === "!") {
        switch (commandName){
            case '!dice':
                const num = rollDice();
                client.say(target, `You rolled a ${num}`);
                console.log(`* Executed ${commandName} command`);
                break;
            case '!lapin':
                client.say(target, `${lapin} lapins ont été oblitérés jusqu'à présent`);
                console.log(`* Executed ${commandName} command`);
                break;
            case '!lapin+':
                lapin++;
                console.log(`* Executed ${commandName} command`);
                break;
        }
    }
    if (commandName.charAt(0) === "!" && commandName.split(" ")[0] === "!lapinadd" && (context.mod || context.username === "happylewie")) {
        if (commandName.split(" ")[1] != null) {
            lapin = lapin + parseInt(commandName.split(" ")[1]);
            console.log(`* lapin add triggered with a number`);
        } else {
            lapin++;
            console.log(`* lapin add triggered with + 1`);
        }
    }
}



// Function called when the "dice" command is issued
function rollDice() {
    const sides = 6;
    return Math.floor(Math.random() * sides) + 1;
}

// Called every time the bot connects to Twitch chat
function onConnectedHandler(addr, port) {
    console.log(`* Connected to ${addr}:${port}`);
}