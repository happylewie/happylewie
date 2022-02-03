const parser = require("gherkin-parse");
const fs = require("fs");
const find = require('find');
let htmlPage = "";

find.file(/\.json$/, `./featureFiles/`, async function (files) {
    for (let f = 0; f < files.length; f++) {
        await fs.readFile(files[f], 'utf-8', (err, jsonData) => {
            if (err) {
                console.log('err reading file ', err);
            }
            let gherkinJson = JSON.parse(jsonData);
            htmlPage = `<html><meta http-equiv="Content-Type" content="text/html; charset=utf-8">
            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-eOJMYsd53ii+scO/bJGFsiCZc+5NDVN2yr8+0RDqr0Ql0h+rP48ckxlpbzKgwra6" crossorigin="anonymous">
            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js" integrity="sha384-JEW9xMcG8R+pH31jmWH6WWP0WintQrMb4s7ZOdauHnUtxwoG2vI5DkLtS3qm9Ekf" crossorigin="anonymous"></script>
            <h1>Projet: Project Name</h1>
            <h2>Feature: ${JSON.parse(jsonData).feature.name}</h2>`
            for (let i = 0; i < gherkinJson.feature.children.length; i++) {
                htmlPage = htmlPage + `
                    <div class="accordion" id="accordionExample">
                       <div class="accordion-item">
                          <h3 class="accordion-header" id="heading${i}">
                             <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${i}" aria-expanded="false" aria-controls="collapse${i}">
                                ${gherkinJson.feature.children[i].name}
                             </button>
                          </h3>
                    <div id="collapse${i}" class="accordion-collapse collapse" aria-labelledby="heading${i}" data-bs-parent="#accordionExample">
                       <div class="accordion-body">
                          <ul>`;
                for (let j = 0; j < gherkinJson.feature.children[i].steps.length; j++) {
                    htmlPage = htmlPage + `
                                <li>
                                    <b>${gherkinJson.feature.children[i].steps[j].keyword}</b>${gherkinJson.feature.children[i].steps[j].text}
                                </li>`
                }
                htmlPage = htmlPage + `
                                </ul>
                                </div>
                            </div>
                        </div>`
            }
            htmlPage = htmlPage + `</div>`
            fs.writeFile(`./featureFilesHTML/${files[f]}.html`, htmlPage, err => {
                if (err) {
                    console.error(err)
                    return
                }
            });
        });
    }
});