require('dotenv').config()
const express = require('express')
const compression = require('compression')
const argon2i = require('argon2-ffi').argon2i;
const app = express()
const crypto = require('crypto')
const jwt = require('jsonwebtoken');
const mongoose = require('mongoose')
const userSchema = require('./models/users')
const nodemailer = require('nodemailer')

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


app.post('/signIn', async (req,res) => {
    crypto.randomBytes(32, function(err, salt){
        if(err){
            console.log(err);
        }
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
                        console.log(req.body)
                    }
                    else{
                        res.send({success: false,
                            error: "Existing credentials"
                          })
                    }
                })
                .catch( (err) => {
                    console.log(err);
                    res.status(err.status || 500).send({
                        success: false,
                        error: {
                            status: err.status || 500,
                            message: "Network error"
                        }
                    })
                })
        })

    })

    
    
});

app.post('/logIn', async (req, res) => {
    var userAccout = await users.findOne({name: req.body.name});
    if(userAccout != null){
        argon2i.verify(userAccout.pass, req.body.pass)
        .then(succeed => {
            if(succeed){
                res.send({success: true});
            }
            else{
                res.send({success: false,
                message: "Not existing user"})
            }
        })
        .catch( (err) => {
            res.send({
                success: false,
                error: {
                    status: err.status || 500,
                    message: err.message
                }
            })
        })
    }
});


//RESET PASSWORD
app.post('/Reset_pass', (request,response,next)=>{
    var post_data = request.body;
    
    var mail = post_data.mail;
    console.log(mail);
    
    var insertJson = {
        'mail': mail
    };
    
    //CHECK EXISTS EMAIL
    userSchema.find({'mail':mail}).count(function(err,number){
        if(number == 0){
            console.log("email n'existe pas");
            response.json("email n'existe pas");
        }
        else
        {
            
            // emetteur
            var smtpTransport = nodemailer.createTransport({
                service: 'gmail', 
                auth: {
                    user: 'anas.9haoud@gmail.com',
                    pass: 'rkvnulksxwzlumwg'
                }
            });

            // code de vérification
            var code_verification = +Math.floor(Math.random()*10000);
            response.json(code_verification);

            //envoie au destinantaire
            var mailOptions = {
                from: 'anas.9haoud@gmail.com',
                to: mail,
                subject: 'Tourist App Password Reset',
                text: 'Votre code de vérification: '+code_verification
            };
            
            smtpTransport.sendMail(mailOptions, function(error, info){
                if (error) {
                  console.log(error);
                } else {
                  console.log('Email sent: ' + info.response);
                }
            }); 
        }
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
