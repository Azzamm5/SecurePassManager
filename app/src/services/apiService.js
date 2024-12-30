// src/services/apiService.js

import axios from 'axios';

// Créer une instance d'Axios
const apiClient = axios.create({
    baseURL: 'http://192.168.0.176:5001/api/Account', // Remplacez par l'URL de votre API
    headers: {
        'Content-Type': 'application/json',
    },
});

// Fonction pour récupérer des données
export const fetchData = async (endpoint) => {
    try {
        const response = await apiClient.get(endpoint);
        return response.data;
    } catch (error) {
        throw error; // Propager l'erreur pour la gérer dans le composant
    }
};

// Vous pouvez ajouter d'autres méthodes (POST, PUT, DELETE) ici si nécessaire

export default apiClient;