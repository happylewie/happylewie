const fs = require('fs');

const USER = '// BITBUCKET USER';
const PASS = '//BIT BUCKET API KEY';
const git = require('simple-git/promise');
const remote = `https://${USER}:${PASS}@`;

const repositories = require('./config.json');

const updateRepositories = async () => {
    for (let everyRepo in repositories.repositories) {
        let dateStamp = new Date(Date.now());
        const REPONAME = repositories.repositories[everyRepo].name;
        // CHECK IF DIRECTORY EXISTS THEN PULL THE LATEST CHANGES
        try {
            await fs.promises.access(`./repositories/${REPONAME}`)
            console.log("\x1b[33m%s\x1b[0m", `${dateStamp} INFO: ${REPONAME} repository already exists pulling repo`);
            await git(`./repositories/${REPONAME}`)
                .pull();
            console.log("\x1b[32m%s\x1b[0m", 'SUCCESS: pulling repo finished');
        } catch (err) {
            // IF THE REPOSITORY WAS ADDED, DELETED, OR SIMPLY NEVER CLONED, CLONE IT THEN SWITCH TO BRANCH develop
            await git()
                .clone(remote + repositories.repositories[everyRepo].clone_url, `./repositories/` + repositories.repositories[everyRepo].name)
                .then(await console.log("\x1b[33m%s\x1b[0m", 'INFO: cloning ' + repositories.repositories[everyRepo].name + ' started'))
                .catch((err) => console.error("\x1b[31m%s\x1b[0m", 'ERROR: failed cloning' + repositories.repositories[everyRepo].name + ': ', err));
            console.log("\x1b[32m%s\x1b[0m", 'SUCCESS: cloning ' + repositories.repositories[everyRepo].name + ' finished');
            await git(`./repositories/` + repositories.repositories[everyRepo].name)
                .checkout("develop");
            console.log('switched branch to develop');
        }
    }
};

updateRepositories();
