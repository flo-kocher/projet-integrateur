require('dotenv').config()
const express = require('express')
const compression = require('compression')
const app = express()
const bcrypt = require('bcrypt')
const mongoose = require('mongoose')
const userSchema = require('./models/users')

app.engine('html', require('ejs').renderFile);
app.set('view engine', 'html');

mongoose.connect(`${process.env.DB_URL}`, { useNewUrlParser: true, useUnifiedTopology: true }, (e) => {
    if(!e){
        console.log('db connected');
    }
    else{
        console.log('db error');
    }
})

app.use(compression())
app.use(express.json());
app.listen(3000, () => console.log('Server has started on port 3000'))


app.post('/signIn', async (req,res) => {

    const user = new userSchema ({
        name: req.body.name,
        mail: req.body.mail,
        pass: req.body.pass
    })

    const newUser = await user.save()
    console.log(req.body)
    res.status(201).json(newUser)
});

app.post('/logIn', async (req,res) =>{
    const user = new userSchema ({
        name: req.body.name,
        mail: req.body.mail,
        pass: req.body.pass
    })

    const newUser = await user.findOne({name: req.body.name, pass: req.body.pass})
    .then( () => {
        console.log("pas d'erreur !")
    })
    .catch( (e) =>{
        console.log(e);
    })
    
})

app.use((err, req, res, next) => {
    console.log(req);
    console.log(err);
    res.status(400).json({message: err.message })
})






