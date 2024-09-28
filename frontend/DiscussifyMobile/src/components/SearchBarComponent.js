import React, { useState } from 'react';
import { Appbar } from 'react-native-paper';

export default function SearchBarComponent() {
    return (
        <Appbar.Header>
            <Appbar.Content title="Discussify" />
            <Appbar.Action icon="magnify" onPress={() => { }} />
        </Appbar.Header>
    );
}
