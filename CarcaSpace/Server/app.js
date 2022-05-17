require('dotenv').config()
const express = require('express')
const compression = require('compression')
const argon2i = require('argon2-ffi').argon2i;
const app = express()
const crypto = require('crypto')
const jwt = require('jsonwebtoken');
const mongoose = require('mongoose')
const userSchema = require('./models/users')
const users = require('./models/users');

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

app.get('/', async (req, res) => {
	res.send("hello world !");
});

app.post('/signIn', async (req,res) => {
    console.log("1");
    console.log(req.body);

    if(!validEmail(req.body.mail)){
        res.send({success: false,
                  message: "Email not valid"});
        return;
    }
    crypto.randomBytes(32, function(err, salt){
        argon2i.hash(req.body.pass, salt)
        .then(async (hash) => {
            const user = new userSchema ({
                name: req.body.name,
                mail: req.body.mail,
                pass: hash,
            });
            userSchema.exists({name: req.body.name, mail: req.body.mail})
                .then(async (doc) => {
                    if(doc == null){
                        const newUser = await user.save()
                        res.send({success: true});
                    }
                    else{
                        res.send({success: false,
                            message: "Existing credentials"
                          })
                    }
                })
                .catch( (err) => {
                    console.log(err);
                    res.send({
                        success: false,
                        error: {
                            status: err.status || 500,
                            message: "Network error"
                        }
                    });
                })
        })
        .catch( (err) => {
            console.log(err)
            res.send({
                sucess: false,
                error: {
                    status: err.status || 500,
                    message: "Hashing error"
                }
            });
        })
    })

    
    
});

app.post('/logIn', async (req, res) => {
    var userAccount = await users.findOne({name: req.body.name});
    console.log(req.body);
    if(userAccount != null){
        console.log("if");
        console.log(userAccount.pass);
        argon2i.verify(userAccount.pass, req.body.pass)
        .then(succeed => {
            if(succeed){
                res.send({success: true, 
			    name: req.body.name});
            }
            else{
                res.send({success: false,
                message: "Error verifying account"})
            }
        })
        .catch( (err) => {
            res.send({
                success: false,
                error: {
                    status: err.status || 500,
                    message: err.message
                }
            });
        })
    }
    else{
        console.log("else");
        res.send({success: false,
                  message: "Not existing account"});
    }
});

app.use(async (err, req, res) => {
    console.log(req.path);
    console.log(req.headers);
    console.log(req);
    //console.log(err);
    res.send({
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
function validEmail (email){
    const emailRegex = /^([A-Za-z0-9_\-.])+@([A-Za-z0-9_\-.])+\.([A-Za-z]{2,4})$/;
    if (!emailRegex.test(email)) return false;
    return true;
}
