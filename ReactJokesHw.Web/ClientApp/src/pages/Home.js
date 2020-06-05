import React from 'react'
import axios from 'axios'
import immer from 'immer'
import { AccountContext } from '../AccountContext';
import { Link } from 'react-router-dom';
class Home extends React.Component {
    state = {
        jokes: []

    }

    componentDidMount = async () => {
        this.refreshJokes();
        console.log(this.state.jokes)
    }
    refreshJokes = async () => {
        const { data } = await axios.get('api/Joke/GetAllJokes');
        this.setState({ jokes: data })
    }
    addJoke = async () => {
        await axios.post('/api/Joke/AddJoke')
        this.refreshJokes();
    }
    onLikeClick = async (joke) => {
        await axios.post('api/joke/AddLike', joke);
        this.refreshJokes();
    }
    onDislikeClick = async (joke) => {
        await axios.post('api/joke/AddDislike', joke);
        this.refreshJokes();
    }
    render() {
        return (
            <AccountContext.Consumer>
                {value => {
                    const { user } = value;
                    const isLoggedIn = !!user;
                    return (
                        <div style={{ backgroundColor: 'white', minHeight: 1000, paddingTop: 10 }}>
                            <div className="row">
                                <button className="btn btn-warning" onClick={this.addJoke}> Add a Joke :) </button>
                            </div>
                            {this.state.jokes.map(j =>
                                <div className='well'>
                                    <h3>Question: {j.setup}</h3>
                                    <br />
                                    <h3>Answer: {j.punchline}</h3>
                                    <br />
                                    {!!j.userjokeLikes ? <h5>Likes: {j.userjokeLikes.filter(ulj => ulj.liked === true).length}</h5> : <h5>Likes: 0</h5>}
                                    {!!j.userjokeLikes ? <h5>Dislikes: {j.userjokeLikes.filter(ulj => ulj.liked === false).length}</h5> : <h5>Dislikes: 0</h5>}
                                    <br />
                                    {!!isLoggedIn && <Link to={`/SpecificJoke${j.id}`}>Like or dislike the joke</Link>} 
                                    ,</div>)}
                        </div>
                    )
                }
                }
            </AccountContext.Consumer>
        )
    }
}
export default Home;

