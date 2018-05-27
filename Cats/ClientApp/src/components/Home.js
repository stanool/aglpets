import React, { Component } from 'react';
import { CatList } from './CatList';

export class Home extends Component {
  displayName = Home.name

  render() {
    return (
        <CatList />
    );
  }
}
