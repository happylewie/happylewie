const parser = require("gherkin-parse");
const fs = require("fs");
const find = require('find');
const configRepo = require('./config.json');


async function writeJsonGherkin() {
    for (let j = 0; j < configRepo.repositories.length; j++) {
        find.file(/\.feature$/, `./repositories/${configRepo.repositories[j].name}/`, async function(files) {
            for (let i = 0; i < files.length; i++) {
                const jsonObject = await parser.convertFeatureFileToJSON(files[i]);
                fs.writeFile(`./featureFiles/${configRepo.repositories[j].name}-feature${i}.json`, JSON.stringify(await jsonObject), err => {
                    if (err) {
                        console.error(err)
                        return
                    }
                });
            }
        });
    }
}

writeJsonGherkin()