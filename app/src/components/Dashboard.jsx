import React, { useEffect, useState } from 'react';
import { X, Plus } from 'lucide-react';
import '../styles/SecurePassManager.css';
import axios from 'axios';

function AccountList() {
  const [accounts, setAccounts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [searchQuery, setSearchQuery] = useState('');

  useEffect(() => {
    fetch('/api/Account')
      .then(response => {
        if (!response.ok) {
          throw new Error('Erreur lors de la récupération des comptes');
        }
        return response.json();
      })
      .then(data => {
        setAccounts(data);
        setLoading(false);
      })
      .catch(err => {
        setError(err.message);
        setLoading(false);
      });
  }, []);

  const filteredAccounts = accounts.filter(account =>
    account.username.toLowerCase().includes(searchQuery.toLowerCase()) ||
    account.service.toLowerCase().includes(searchQuery.toLowerCase()) 
  
  );

  const clearSearch = () => {
    setSearchQuery('');
  };

  if (loading) {
    return <div className="loading-indicator">Chargement...</div>;
  }

  if (error) {
    return <div className="error-message">Erreur : {error}</div>;
  }

  return (
    <div className="securepass-container">
      <div className="securepass-header">
        <div className="search-container">
          <input
            type="text"
            className="search-input"
            placeholder="Rechercher un compte..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
          />
          {searchQuery && (
            <button className="clear-search-button" onClick={clearSearch}>
              <X size={16} />
            </button>
          )}
        </div>
        <button className="add-button">
          <Plus size={20} />
          Ajouter un compte
        </button>
      </div>
      <div className="account-list">
        {filteredAccounts.length > 0 ? (
          filteredAccounts.map((account, index) => (
            <div key={index} className="account-item">
              <h3 className="account-item-title">{account.username}</h3>
              <p className="account-item-subtitle">{account.service}</p>
              <p className="account-item-subtitle">{account.Dar}</p>
            </div>
          ))
        ) : (
          <div className="no-results">
            <p>Aucun compte trouvé</p>
          </div>
        )}
      </div>
    </div>
  );
}

export default AccountList;
