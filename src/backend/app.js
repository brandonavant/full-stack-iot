// Copyright (c) Brandon Avant. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

const express = require('express');
const app = express();
const port = 3000;

app.get('/', (req, res) => {});

app.listen(port, () => console.log(`Node server listening on port ${port}...`));
