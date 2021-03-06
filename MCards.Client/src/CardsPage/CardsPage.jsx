import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

import { cardActions } from '../_actions';

class CardsPage extends React.Component {
    componentDidMount() {
        this.props.dispatch(cardActions.getAll());
    }

    // handleDeleteUser(id) {
    //     return (e) => this.props.dispatch(userActions.delete(id));
    // }

    render() {
        const { card, cards } = this.props;
        return (
            <div className="col-md-6 col-md-offset-3">
                <h3>All Cards</h3>
                {cards.items &&
                    <ul>
                        {cards.items.map((card, index) =>
                            <li key={card.pK_Card}>
                                {card.pK_Card + ' ' + card.cardName}
                               
                            </li>
                        )}
                    </ul>
                }
                {/* <p>You're logged in with React!!</p>
                <h3>All registered users:</h3>
                {users.loading && <em>Loading users...</em>}
                {users.error && <span className="text-danger">ERROR: {users.error}</span>}
                {users.items &&
                    <ul>
                        {users.items.map((user, index) =>
                            <li key={user.id}>
                                {user.firstName + ' ' + user.lastName}
                                {
                                    user.deleting ? <em> - Deleting...</em>
                                    : user.deleteError ? <span className="text-danger"> - ERROR: {user.deleteError}</span>
                                    : <span> - <a onClick={this.handleDeleteUser(user.id)}>Delete</a></span>
                                }
                            </li>
                        )}
                    </ul>
                }
                <p>
                    <Link to="/login">Logout</Link>
                </p> */}
            </div>
        );
    }
}

function mapStateToProps(state) {
    return {
        cards: state.cards
    };
    // const { users, authentication } = state;
    // const { user } = authentication;
    // return {
    //     user,
    //     users
    // };
}

const connectedCardsPage = connect(mapStateToProps)(CardsPage);
export { connectedCardsPage as CardsPage };