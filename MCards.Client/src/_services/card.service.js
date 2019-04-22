import config from 'config';
import { authHeader } from '../_helpers';

export const cardService = {
    getAll,
    getById,
    add,
    update,
    delete: _delete
};


function getAll() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/card`, requestOptions).then(handleResponse);
}

function getById(id) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/card/${id}`, requestOptions).then(handleResponse);
}

function add(card) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(card)
    };

    return fetch(`${config.apiUrl}/card/add`, requestOptions).then(handleResponse);
}

function update(card) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(card)
    };

    return fetch(`${config.apiUrl}/card/${card.id}`, requestOptions).then(handleResponse);;
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id) {
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/card/${id}`, requestOptions).then(handleResponse);
}

function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);
       
        if (!response.ok) {
            if (response.status === 401) {
                // auto logout if 401 response returned from api TODO
               // logout();
               // location.reload(true);
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}