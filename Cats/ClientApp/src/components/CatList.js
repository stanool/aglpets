import React, { Component } from 'react';

export class CatList extends Component {
    displayName = CatList.name

  constructor(props) {
    super(props);
    this.state = { owners: [], loading: true, error: false };
  }


    componentDidMount() {
        fetch('api/CatList')
            .then(response => {
                if (response.ok)
                    return response.json();
                throw new Error();
            })
            .then(data => {
                this.setState({ owners: data, loading: false });
            }).catch(() => {
                this.setState({ error: true });
            });
    }

    static renderCatList(owners) {
        return owners.map(owner =>
            <div>
                <h4>{owner.gender} <span className="badge badge-pill">{owner.catNames.length}</span></h4>
            <ul className="list-group">
                {owner.catNames.map(name =>
                    <li className="list-group-item">{name}</li>
                )
                }
                </ul>
            </div>);
        }

    static renderServerError() {
        return (
            <div class="alert alert-danger" role="alert">
                <p><strong>There was an error retrieving data from the server. Please try again later.</strong></p>
            </div>
                );
    };
    render() {
        let contents = this.state.error
            ? CatList.renderServerError()
            : this.state.loading
            ? <p><em>Loading...</em></p>
            : CatList.renderCatList(this.state.owners);

    return (
      <div>
        {contents}
      </div>
    );
  }
}
