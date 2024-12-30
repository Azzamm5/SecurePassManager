import React, { useState } from "react";
import { FaEye, FaEyeSlash, FaSyncAlt } from "react-icons/fa";
import { useNavigate } from "react-router-dom"; // Import du hook pour la redirection
import { GoogleOAuthProvider, GoogleLogin } from "@react-oauth/google";
import "../styles/SignUp.css";
import Image from "../assets/2.png";
import Logo from "../assets/2.png";

const SignUp = () => {
  const [showPassword, setShowPassword] = useState(false);
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [generatedPassword, setGeneratedPassword] = useState("");
  const [passwordError, setPasswordError] = useState("");
  const navigate = useNavigate(); // Hook pour la redirection

  // Fonction pour gérer le succès de la connexion avec Google
  const handleGoogleSuccess = (response) => {
    console.log("Connexion Google réussie :", response);
    navigate("/dashboard"); // Redirection vers le tableau de bord
  };

  const handleGoogleError = () => {
    console.log("La connexion Google a échoué");
  };

  // Fonction pour générer un mot de passe
  const generatePassword = () => {
    const characters =
      "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
    const passwordLength = Math.floor(Math.random() * (16 - 8 + 1)) + 8; // Longueur entre 8 et 16 caractères
    let newPassword = "";
    for (let i = 0; i < passwordLength; i++) {
      newPassword += characters.charAt(Math.floor(Math.random() * characters.length));
    }
    setGeneratedPassword(newPassword);
    setPassword(newPassword); // Remplir le champ de mot de passe avec le mot de passe généré
    setConfirmPassword(newPassword); // Remplir aussi le champ de confirmation avec le même mot de passe
    setPasswordError(""); // Réinitialiser l'erreur de confirmation
  };


  // Vérifier si les mots de passe correspondent et que tous les champs sont remplis
  const handleSubmit = (e) => {
    e.preventDefault();

    // Vérifier si tous les champs sont remplis
    if (!password || !confirmPassword) {
      setPasswordError("Please fill in all fields.");
      return;
    }

    // Vérifier si les mots de passe correspondent
    if (password !== confirmPassword) {
      alert("Passwords do not match"); // Afficher une boîte de dialogue si les mots de passe ne correspondent pas
    } else {
      setPasswordError("");
      // Soumettre le formulaire
      console.log("Form submitted successfully");
    }
  };

  return (
    <div className="signup-main">
      <div className="signup-left">
        <img src={Image} alt="signup" />
      </div>
      <div className="signup-right">
        <div className="signup-right-container">
          <div className="signup-center">
            <h2>Create Your Account</h2>
            <p>Please enter your details</p>
            <form onSubmit={handleSubmit}>
              <input
                type="email"
                placeholder="Email"
                required
              />
              <div className="pass-input-div">
                <FaSyncAlt 
                  className="generate-password-icon"
                  onClick={generatePassword} 
                />
                <input
                  type={showPassword ? "text" : "password"}
                  value={password}
                  placeholder="Password"
                  onChange={(e) => setPassword(e.target.value)}
                  required
                />
                {showPassword ? (
                  <FaEyeSlash onClick={() => setShowPassword(!showPassword)} />
                ) : (
                  <FaEye onClick={() => setShowPassword(!showPassword)} />
                )}
              </div>
              <div className="pass-input-div">
                <input
                  type={showPassword ? "text" : "password"}
                  value={confirmPassword}
                  placeholder="Confirm Password"
                  onChange={(e) => setConfirmPassword(e.target.value)}
                  required
                />
              </div>
              {passwordError && <p className="error-message">{passwordError}</p>}
              <div className="signup-center-buttons">
                <button type="submit">Sign Up</button>

                <GoogleOAuthProvider clientId="276508008629-hs1uinmn8ui8de6gvbjhtd5klh40pruv.apps.googleusercontent.com">
                  <GoogleLogin
                    onSuccess={handleGoogleSuccess} // Appel de la fonction en cas de succès
                    onError={handleGoogleError}
                  />
                </GoogleOAuthProvider>
              </div>
            </form>
          </div>
          <p className="signup-bottom-p">
            Already have an account? <a href="/login">Login</a>
          </p>
        </div>
      </div>
    </div>
  );
};

export default SignUp;
