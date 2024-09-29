import React from 'react';
import { Appbar } from 'react-native-paper';
import { theme } from '../components/theme';

export default function SearchBarComponent() {
    return (
        <Appbar.Header style={{ backgroundColor: theme.background }}>
            <Appbar.Content title="Discussify" titleStyle={{ color: theme.iconActiveColor, fontWeight: 'bold' }} />
            <Appbar.Action icon="magnify" color={theme.iconColor} onPress={() => { }} />
        </Appbar.Header>
    );
}
