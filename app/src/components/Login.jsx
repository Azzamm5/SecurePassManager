import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom"; // Import de useNavigate pour la redirection
import Image from "../assets/1.png";
import Logo from "../assets/1.png";
import GoogleSvg from "../assets/icons8-google.svg";
import { FaEye } from "react-icons/fa6";
import { FaEyeSlash } from "react-icons/fa6";
import "../styles/Login.css"; // Fichier CSS propre à Login
import { GoogleOAuthProvider, GoogleLogin } from "@react-oauth/google";

const Login = () => {
  const [showPassword, setShowPassword] = useState(false);
  const navigate = useNavigate(); // Hook pour la redirection

  const handleSuccess = (response) => {
    console.log("Connexion réussie :", response);
    // Redirection vers Dashboard après connexion réussie
    navigate("/dashboard");
  };

  const handleError = () => {
    console.log("La connexion a échoué");
  };

  

  return (
    <div className="login-main">
      <div className="login-left">
        <img src={Image} alt="Login" />
      </div>
      <div className="login-right">
        <div className="login-right-container">
          <div className="login-center">
            <h2>Welcome back</h2>
            <p>Please enter your details</p>
            <form>
              <input type="email" placeholder="Email" />
              <div className="pass-input-div">
                <input
                  type={showPassword ? "text" : "password"}
                  placeholder="Password"
                />
                {showPassword ? (
                  <FaEyeSlash onClick={() => setShowPassword(!showPassword)} />
                ) : (
                  <FaEye onClick={() => setShowPassword(!showPassword)} />
                )}
              </div>

              <div className="login-center-options">
                <div className="remember-div">
                  <input type="checkbox" id="remember-checkbox" />
                  <label htmlFor="remember-checkbox">Remember me</label>
                </div>
              </div>

              <div className="login-center-buttons">
                <Link to="/dashboard">
                  <button type="button">Log In</button>
                </Link>

                <GoogleOAuthProvider clientId="276508008629-hs1uinmn8ui8de6gvbjhtd5klh40pruv.apps.googleusercontent.com">
                  <GoogleLogin
                    onSuccess={handleSuccess} // Appel de la fonction handleSuccess en cas de succès
                    onError={handleError}
                  />
                </GoogleOAuthProvider>
              </div>
            </form>
          </div>

          <p className="login-bottom-p">
            Don't have an account? <Link to="/signup">Sign Up</Link>
          </p>
        </div>
      </div>
    </div>
  );
};

export default Login;



























// import React, { useState } from "react";
// import { Link, useNavigate } from "react-router-dom"; // Pour la navigation
// import axios from "axios"; // Importer Axios
// import Image from "../assets/1.png";
// import Logo from "../assets/1.png";
// import GoogleSvg from "../assets/icons8-google.svg";
// import { FaEye, FaEyeSlash } from "react-icons/fa6";
// import "../styles/Login.css";
// import { GoogleOAuthProvider, GoogleLogin } from "@react-oauth/google";

// const Login = () => {
//   const [email, setEmail] = useState(""); // Champ email
//   const [password, setPassword] = useState(""); // Champ mot de passe
//   const [showPassword, setShowPassword] = useState(false); // Affichage du mot de passe
//   const [error, setError] = useState(null); // Gestion des erreurs
//   const navigate = useNavigate(); // Pour la redirection

//   // Fonction de gestion de soumission du formulaire
//   const handleSubmit = async (e) => {
//     e.preventDefault();
//     setError(null); // Réinitialiser les erreurs

//     try {
//       // Appeler l'API de connexion avec Axios
//       const response = await axios.post("http://192.168.0.176:5001/api/Account/login", {
//         email,
//         password,
//       });

//       // Si la réponse est réussie, rediriger vers le dashboard
//       console.log("Connexion réussie :", response.data);
//       navigate("/dashboard");
//     } catch (err) {
//       // Gestion des erreurs
//       console.error("Erreur lors de la connexion :", err.response?.data || err.message);
//       setError(err.response?.data?.message || "Une erreur est survenue.");
//     }
//   };

//   return (
//     <div className="login-main">
//       <div className="login-left">
//         <img src={Image} alt="Login" />
//       </div>
//       <div className="login-right">
//         <div className="login-right-container">
//           <div className="login-center">
//             <h2>Welcome back</h2>
//             <p>Please enter your details</p>
//             <form onSubmit={handleSubmit}>
//               {/* Champ Email */}
//               <input
//                 type="email"
//                 placeholder="Email"
//                 value={email}
//                 onChange={(e) => setEmail(e.target.value)} // Mettre à jour l'état local
//                 required
//               />

//               {/* Champ Password */}
//               <div className="pass-input-div">
//                 <input
//                   type={showPassword ? "text" : "password"}
//                   placeholder="Password"
//                   value={password}
//                   onChange={(e) => setPassword(e.target.value)} // Mettre à jour l'état local
//                   required
//                 />
//                 {showPassword ? (
//                   <FaEyeSlash onClick={() => setShowPassword(!showPassword)} />
//                 ) : (
//                   <FaEye onClick={() => setShowPassword(!showPassword)} />
//                 )}
//               </div>

//               {/* Afficher les erreurs */}
//               {error && <p className="error-message">{error}</p>}

//               <div className="login-center-options">
//                 <div className="remember-div">
//                   <input type="checkbox" id="remember-checkbox" />
//                   <label htmlFor="remember-checkbox">Remember me</label>
//                 </div>
//               </div>

//               {/* Bouton de soumission */}
//               <div className="login-center-buttons">
//                 <button type="submit">Log In</button>

//                 {/* Connexion Google */}
//                 <GoogleOAuthProvider clientId="276508008629-hs1uinmn8ui8de6gvbjhtd5klh40pruv.apps.googleusercontent.com">
//                   <GoogleLogin
//                     onSuccess={(response) => {
//                       console.log("Connexion réussie avec Google :", response);
//                       navigate("/dashboard");
//                     }}
//                     onError={() => console.log("La connexion Google a échoué")}
//                   />
//                 </GoogleOAuthProvider>
//               </div>
//             </form>
//           </div>

//           <p className="login-bottom-p">
//             Don't have an account? <Link to="/signup">Sign Up</Link>
//           </p>
//         </div>
//       </div>
//     </div>
//   );
// };

// export default Login;
