const { json } = require('express')
const express = require('express')
const fs = require('fs')
const bodyParser = require('body-parser')
const app = express()
const port = 3000
var jsonParser = bodyParser.json()
var cors = require('cors')

app.use(cors())

app.get('/', (req, res) => {
  res.send(req.headers)
})

app.post('/intergrates/register', jsonParser,  (req, res) => {
  var timeStamp = new Date();
  fs.writeFile('./registerJson.txt', `${timeStamp.toString()}: ${JSON.stringify(req.body)}`,  function (err) {
    if(err) {
      console.log("error");
    }else{
      res.status(200).send("Ok");
    }
  });
})

app.listen(port, () => {
  console.log(`Example app listening at http://localhost:${port}`)
})