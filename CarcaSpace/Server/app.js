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


app.post('/signIn', async (req,res,next) => {
    console.log(req);
    console.log("1")
    /*
        const user = new userSchema ({
            name: 'On est pas d pd',
            pass: 'oe d gros zizi'
    })
            const newUser = await user.save()
            res.status(201).json(newUser)
            console.log(res);*/
});

app.use((err, req, res, next) => {
    console.log(req);
    console.log(e);
    res.status(400).json({message: e.message })
})

app.get('/signIn', async (req,res) => {
    new Promise( (resolve,reject) => {
        res.render('Hello');
    }).then((e) =>{
        console.log("youpi");
    }).catch((e) =>{
        console.log("pas youpi");
    })
})






