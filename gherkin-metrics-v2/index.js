const fs = require('fs');
const glob = require('glob');

const USER = //bitbucket user;
const PASS = //bitbucket API KEY;
const git = require('simple-git/promise');
const remote = `https://${USER}:${PASS}@`;

const repositories = require('./config.json');

let dynaTags = [];
let allDynaTags = [];
let repoRound = 0;
let allScenarios = [];

const gherkin = require('gherkin');
const options = {
    includeSource: true,
    includeGherkinDocument: true,
    includePickles: true,
}

const gherkinParser = new gherkin.Parser();

const updateRepositories = async () => {
    for (let everyRepo in repositories.repositories) {
        let dateStamp = new Date(Date.now());
        const REPONAME = repositories.repositories[everyRepo].name;
        // CHECK IF DIRECTORY EXISTS THEN PULL THE LATEST CHANGES
        try {
            await fs.promises.access(`./repositories/${REPONAME}`)
            console.log("\x1b[33m%s\x1b[0m", `${dateStamp} INFO: ${REPONAME} repository already exists pulling repo`);
            await git(`./repositories/${REPONAME}`)
                .silent(false)
                .pull();
            console.log("\x1b[32m%s\x1b[0m", 'SUCCESS: pulling repo finished');
        } catch (err) {
            // IF THE REPOSITORY WAS ADDED, DELETED, OR SIMPLY NEVER CLONED, CLONE IT THEN SWITCH TO BRANCH develop
            await git().silent(false)
                .clone(remote + repositories.repositories[everyRepo].clone_url, `./repositories/` + repositories.repositories[everyRepo].name)
                .then(await console.log("\x1b[33m%s\x1b[0m", 'INFO: cloning ' + repositories.repositories[everyRepo].name + ' started'))
                .catch((err) => console.error("\x1b[31m%s\x1b[0m", 'ERROR: failed cloning' + repositories.repositories[everyRepo].name + ': ', err));
            console.log("\x1b[32m%s\x1b[0m", 'SUCCESS: cloning ' + repositories.repositories[everyRepo].name + ' finished');
            await git(`./repositories/` + repositories.repositories[everyRepo].name)
                .silent(false)
                .checkout("develop");
            console.log('switched branch to develop');
        }
    }
};

const addCount = async(tag, repo) => {

    for (tagLoop in allDynaTags) {
        if (allDynaTags[tagLoop].name === tag) {
            for (allRepo in dynaTags)
            {
                if (dynaTags[allRepo].name === repo[0]) {
                    dynaTags[allRepo].tags[tagLoop].count += 1;
                }
            }
            allDynaTags[tagLoop].count += 1;
        }
    }
};

const makeTags = async () => {
    for(allRepos in repositories.repositories) {
        dynaTags.push({"name":repositories.repositories[allRepos].name, "tags":[]});
            for(allTags in repositories.tags){
                dynaTags[allRepos].tags.push({"name":repositories.tags[allTags].name,"count":repositories.tags[allTags].count});
            }
    }
    for(allTags in repositories.tags){
        allDynaTags.push({"name":repositories.tags[allTags].name,"count":repositories.tags[allTags].count});
    }
    await scrapeFeatureFiles();
}

const countTags = async (featureFile) => {

        await fs.readFile(featureFile, 'utf8', async (err, data) => {
            if (err) throw err;
            let result = await gherkinParser.parse(data);
            let repository = featureFile.split('/').slice(2,3);
            let file = repository+"--"+featureFile.split('/').slice(-1);
            // console.log(file);
                for (let featuresTags in result.feature.children) {
                try {
                    switch (result.feature.children[featuresTags].type) {
                        case ("Background"):
                            break;
                        case ("Scenario"):
                            allScenarios.push({"name": result.feature.children[featuresTags].name, "tags": []});

                            if (result.feature.tags.length === 0) {
                                await addCount("@automated", repository);
                                allScenarios[allScenarios.length - 1].tags.push({"name": "@automated"})
                            }
                            for (let featuresTagsNames in result.feature.tags)
                            {
                                // console.log(result.feature.tags[featuresTagsNames].name);
                                await addCount(result.feature.tags[featuresTagsNames].name, repository);
                                allScenarios[allScenarios.length - 1].tags.push({"name": result.feature.tags[featuresTagsNames].name})
                            }
                            if (result.feature.children[featuresTags].tags.length > 0) {
                                for (let featuresNumbers in result.feature.children[featuresTags].tags) {
                                    // console.log(result.feature.children[featuresTags].tags[featuresNumbers].name);
                                    await addCount(result.feature.children[featuresTags].tags[featuresNumbers].name, repository);
                                    allScenarios[allScenarios.length - 1].tags.push({"name": result.feature.children[featuresTags].tags[featuresNumbers].name})
                                }
                            } else {
                                await addCount("@automated", repository);
                            }
                            break;
                        case ("ScenarioOutline"):
                            for (numberOfExamples in result.feature.children[featuresTags].examples[0].tableBody) {
                                allScenarios.push({"name": result.feature.children[featuresTags].name + "exemple No." + numberOfExamples, "tags": []});
                                if (result.feature.tags.length === 0) {
                                    await addCount("@automated", repository);
                                    allScenarios[allScenarios.length - 1].tags.push({"name": "@automated"})
                                }
                                for (let featuresTagsNames in result.feature.tags) {
                                    // console.log(result.feature.tags[featuresTagsNames].name);
                                    await addCount(result.feature.tags[featuresTagsNames].name, repository);
                                    allScenarios[allScenarios.length - 1].tags.push({"name": result.feature.tags[featuresTagsNames].name})
                                }
                                if (result.feature.children[featuresTags].tags.length > 0) {

                                    for (let featuresNumbers in result.feature.children[featuresTags].tags) {
                                        // console.log(result.feature.children[featuresTags].tags[featuresNumbers].name);
                                        await addCount(result.feature.children[featuresTags].tags[featuresNumbers].name, repository);
                                        allScenarios[allScenarios.length - 1].tags.push({"name": result.feature.children[featuresTags].tags[featuresNumbers].name})
                                    }
                                } else {
                                    await addCount("@automated", repository);
                                }
                            }
                            break;
                    }
                } catch (err) {
                    console.error(err +" in "+result.feature.children[featuresTags].name);
                }
            }
        });

}

const scrapeFeatureFiles = async() =>  {
    glob(`./repositories/**/*.feature`, {}, async(err, files) => {
        for (let featureFiles in files) {
            await countTags(files[featureFiles]);
        }
    });
};

updateRepositories();

process.on('beforeExit', (code) => {

    for (allRepo in dynaTags) {
        console.log(dynaTags[allRepo]);
    }
    console.log(allDynaTags);
/*   fs.writeFile("test.html",generateHTML, function(err) {
        if (err) {
            console.log(err);
        }
    });*/

});