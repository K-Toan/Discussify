import React from 'react';
import { View, Text, FlatList, Image, TouchableOpacity, StyleSheet } from 'react-native';
import { theme } from '../components/theme';

const notifications = [
    {
        id: 1,
        avatar: 'https://picsum.photos/100',
        type: 'like',
        message: 'JohnDoe liked your post.',
        time: '2h ago',
        read: false,
    },
    {
        id: 2,
        avatar: 'https://picsum.photos/101',
        type: 'comment',
        message: 'JaneSmith commented on your post.',
        time: '5h ago',
        read: true,
    },
    {
        id: 3,
        avatar: 'https://picsum.photos/102',
        type: 'follow',
        message: 'GameLover started following you.',
        time: '1d ago',
        read: false,
    },
];

export default function NotificationsScreen() {
    return (
        <View style={styles.container}>
            <FlatList
                data={notifications}
                keyExtractor={(item) => item.id.toString()}
                renderItem={({ item }) => (
                    <TouchableOpacity style={[styles.notification, item.read ? styles.read : styles.unread]}>
                        <Image source={{ uri: item.avatar }} style={styles.avatar} />
                        <View style={styles.textContainer}>
                            <Text style={styles.message}>{item.message}</Text>
                            <Text style={styles.time}>{item.time}</Text>
                        </View>
                    </TouchableOpacity>
                )}
            />
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: theme.background,
        paddingHorizontal: 10,
        paddingVertical: 10,
    },
    notification: {
        flexDirection: 'row',
        alignItems: 'center',
        padding: 10,
        borderBottomWidth: 1,
        borderColor: theme.borderColor,
    },
    avatar: {
        width: 40,
        height: 40,
        borderRadius: 20,
        marginRight: 10,
    },
    textContainer: {
        flex: 1,
    },
    message: {
        color: theme.text,
        fontSize: 16,
        fontWeight: 'bold',
    },
    time: {
        color: theme.subtext,
        fontSize: 12,
    },
    unread: {
        backgroundColor: '#f2f2f2',
    },
    read: {
        backgroundColor: theme.background,
    },
});
