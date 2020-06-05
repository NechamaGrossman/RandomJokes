import React from 'react'
import axios from 'axios'
import immer, { produce } from 'immer'
import { Link } from 'react-router-dom';

class SpecificJoke extends React.Component {
    state = {
        joke: {
            setup: '',
            punchline: '',
            id: '',
            likesCount: '',
            dislikesCount: ''
        },
        userInteractionStatus: '',
        intervalObj: ''
    }
    componentDidMount = async () => {
        const { data } = await axios.get(`/api/joke/GetJokeForId?JokeId=${this.props.match.params.Id}`);
        const { data: interactionstatus } = await axios.get(`/api/joke/GetStatusOfUser?JokeId=${this.props.match.params.Id}`);
        this.setState({ joke: data, userInteractionStatus: interactionstatus.status });
        const intervalObj = setInterval(() => {
            this.updateCounts();
        }, 500);
        this.setState({ intervalObj });
    }

    componentWillUnmount = () => {
        clearInterval(this.state.intervalObj);
    }
    updateCounts =async () => {
        const { id } = this.state.joke;
        const { data } = await axios.get(`/api/joke/GetLikesAndDislikesCount?JokeId=${id}`);
        const nextState = produce(this.state ,draft => {
            draft.joke.likesCount = data.likesCount
            draft.joke.dislikesCount=data.dislikesCount
        });
        this.setState(nextState);
    }
    onLikeClick = async (joke) => {
        await axios.post('api/joke/AddLike', joke);
        const { data: interactionstatus } = await axios.get(`/api/joke/GetStatusOfUser?JokeId=${this.props.match.params.Id}`);
        this.setState({ userInteractionStatus: interactionstatus.status });
    }
    onDislikeClick = async (joke) => {
        await axios.post('api/joke/AddDislike', joke);
        const { data: interactionstatus } = await axios.get(`/api/joke/GetStatusOfUser?JokeId=${this.props.match.params.Id}`);
        this.setState({ userInteractionStatus: interactionstatus.status });
    }
    render() {
        const { setup, punchline, dislikesCount, likesCount } = this.state.joke;
        const { userInteractionStatus } = this.state
        const canLike = userInteractionStatus !== 0 && userInteractionStatus !== 3;
        const canDislike = userInteractionStatus !== 1 && userInteractionStatus !== 3;
        return (
            <div style={{ backgroundColor: 'white', minHeight: 1000, paddingTop: 10 }}>
                    <div className='well'>
                        <h3>Question: {setup}</h3>
                        <br />
                    <h3>Answer: {punchline}</h3>
                    <h5>Likes: {likesCount}</h5>
                    <h5>Dislike: {dislikesCount}</h5>
                    <button disabled={!canLike} onClick={() => this.onLikeClick(this.state.joke)} className="btn btn-primary">Like</button>
                    <button disabled={!canDislike} onClick={() => this.onDislikeClick(this.state.joke)} className="btn btn-danger">Dislike</button>
                    <br />
                </div>
            </div>
        )
    }
}
export default SpecificJoke;
