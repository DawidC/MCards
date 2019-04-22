import { cardConstants } from '../_constants';
import { cardService } from '../_services';
import { alertActions } from './';


export const cardActions = {

    getAll,
    delete: _delete
};


function getAll() {
    return dispatch => {
        dispatch(request());

        cardService.getAll()
            .then(
                cards => dispatch(success(cards)),
                error => dispatch(failure(error.toString()))
            );
    };

    function request() {
        console.log('request=uddd')
        return { type: cardConstants.GETALL_REQUEST } }
    function success(cards) { 
        console.log('cardsdawd=uddd')
        return { 
        type: cardConstants.GETALL_SUCCESS, cards
     } }
    function failure(error) { return { type: cardConstants.GETALL_FAILURE, error } }
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id) {
    return dispatch => {
        dispatch(request(id));

        cardService.delete(id)
            .then(
                card => dispatch(success(id)),
                error => dispatch(failure(id, error.toString()))
            );
    };

    function request(id) { return { type: cardConstants.DELETE_REQUEST, id } }
    function success(id) { return { type: cardConstants.DELETE_SUCCESS, id } }
    function failure(id, error) { return { type: cardConstants.DELETE_FAILURE, id, error } }
}