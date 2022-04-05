require('dotenv').config()
const express = require('express')
const compression = require('compression')
const app = express()
const bcrypt = require('bcrypt')
const jwt = require('jsonwebtoken');
const mongoose = require('mongoose')
const userSchema = require('./models/users')
const fs = require("fs");

app.engine('html', require('ejs').renderFile);
app.set('view engine', 'html');

mongoose.connect(`${process.env.DB_URL}`, { useNewUrlParser: true, useUnifiedTopology: true }, (e) => {
    if(!e){
        console.log('db connected');
        
    }
    else{
        console.log('db error');
        console.log(e);
        
    }
})

app.use(compression());
app.use(express.json());
app.listen(process.env.PORT || 3000, () => console.log(`Server has started on port ${process.env.PORT}`))


app.post('/signIn', async (req,res) => {

    const user = new userSchema ({
        name: req.body.name,
        mail: req.body.mail,
        pass: bcrypt.hashSync(req.body.pass, 10)
    });

    userSchema.exists({name: req.body.name, mail: req.body.mail})
        .then(async () => {
            const newUser = await user.save()
            console.log(req.body)
        })
        .catch( (err) => {
            console.log(err);
            res.status(err.status || 500).send({
                success: false,
                error: {
                    status: err.status || 500,
                    message: "User exists"
                }
            })
        })
});

app.post('/logIn', async (req, res) => {
    userSchema.exists({name: req.body.name})
    .then( (doc) => {
        if(doc != null){
            console.log("Result :", doc)
            res.send({success: true});
        }
        else {
            res.send({success: false,
                message: "Not existing user"
            })
        }
    })
    .catch( (err) => {
        console.log(err);
        res.send({
            success: false,
            error: {
                status: err.status || 500,
                message: err.message
            }
        })
    })
});

app.use((err, req, res) => {
    console.log(req);
    console.log(err);
    res.status(err.status || 500).send({
        success: false,
        error: {
            status: err.status || 500,
            message: err.message
        }
    })
})


const tokenHandler = async (user) => {
    try{
        const accessToken = await signJwtToken(user, {
            secret: process.env.JWT_ACCESS_TOKEN_SECRET,
            expiresIn: process.env.JWT_EXPIRY
        })
        return Promise.resolve(accessToken)
    } catch(e){
        return Promise.reject(error)
    }
}
