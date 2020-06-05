import React, { Component } from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Signup from './pages/Signup';
import Login from './pages/Login';
import Logout from './pages/Logout';
import Home from './pages/Home'
import SpecificJoke from './pages/SpecificJoke'
import { AccountContextComponent } from './AccountContext';
import PrivateRoute from './PrivateRoute';

export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <AccountContextComponent>
                <Layout>
                    <Route exact path='/' component={Home}/>
                    <Route exact path='/signup' component={Signup} />
                    <Route exact path='/login' component={Login} />
                    <Route exact path='/Logout' component={Logout} />
                    <Route exact path='/SpecificJoke:Id' component={SpecificJoke}/>
                </Layout>
            </AccountContextComponent>
        );
    }
}
